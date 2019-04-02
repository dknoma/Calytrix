using System;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

namespace UnityEngine
{
	[Serializable]
	[CreateAssetMenu]
	public class RuleTile : TileBase
	{
		public Sprite mDefaultSprite;
		public Tile.ColliderType mDefaultColliderType = Tile.ColliderType.Sprite;

		[Serializable]
		public class TilingRule
		{
			public Neighbor[] mNeighbors;
			public Sprite[] mSprites;
			public float mAnimationSpeed;
			public float mPerlinScale;
			public Transform mRuleTransform;
			public OutputSprite mOutput;
			public Tile.ColliderType mColliderType;
			public Transform mRandomTransform;
			
			public TilingRule()
			{
				mOutput = OutputSprite.Single;
				mNeighbors = new Neighbor[8];
				mSprites = new Sprite[1];
				mAnimationSpeed = 1f;
				mPerlinScale = 0.5f;
				mColliderType = Tile.ColliderType.Sprite;

				for(int i=0; i<mNeighbors.Length; i++)
					mNeighbors[i] = Neighbor.DontCare;
			}

			public enum Transform { Fixed, Rotated, MirrorX, MirrorY }
			public enum Neighbor { DontCare, This, NotThis }
			public enum OutputSprite { Single, Random, Animation }
		}

		[HideInInspector] public List<TilingRule> mTilingRules;

		public override void GetTileData(Vector3Int position, ITilemap tileMap, ref TileData tileData)
		{
			tileData.sprite = mDefaultSprite;
			tileData.colliderType = mDefaultColliderType;
			tileData.flags = TileFlags.LockTransform;
			tileData.transform = Matrix4x4.identity;
			
			foreach (TilingRule rule in mTilingRules)
			{
				Matrix4x4 transform = Matrix4x4.identity;
				if (RuleMatches(rule, position, tileMap, ref transform))
				{
					switch (rule.mOutput)
					{
							case TilingRule.OutputSprite.Single:
							case TilingRule.OutputSprite.Animation:
								tileData.sprite = rule.mSprites[0];
							break;
							case TilingRule.OutputSprite.Random:
								int index = Mathf.Clamp(Mathf.FloorToInt(GetPerlinValue(position, rule.mPerlinScale, 100000f) * rule.mSprites.Length), 0, rule.mSprites.Length - 1);
								tileData.sprite = rule.mSprites[index];
								if (rule.mRandomTransform != TilingRule.Transform.Fixed)
									transform = ApplyRandomTransform(rule.mRandomTransform, transform, rule.mPerlinScale, position);
							break;
					}
					tileData.transform = transform;
					tileData.colliderType = rule.mColliderType;
					break;
				}
			}
		}

		private static float GetPerlinValue(Vector3Int position, float scale, float offset)
		{
			return Mathf.PerlinNoise((position.x + offset) * scale, (position.y + offset) * scale);
		}

		public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
		{
			foreach (TilingRule rule in mTilingRules)
			{
				Matrix4x4 transform = Matrix4x4.identity;
				if (RuleMatches(rule, position, tilemap, ref transform) && rule.mOutput == TilingRule.OutputSprite.Animation)
				{
					tileAnimationData.animatedSprites = rule.mSprites;
					tileAnimationData.animationSpeed = rule.mAnimationSpeed;
					return true;
				}
			}
			return false;
		}
		
		public override void RefreshTile(Vector3Int location, ITilemap tileMap)
		{
			if (mTilingRules != null && mTilingRules.Count > 0)
			{
				for (int y = -1; y <= 1; y++)
				{
					for (int x = -1; x <= 1; x++)
					{
						base.RefreshTile(location + new Vector3Int(x, y, 0), tileMap);
					}
				}
			}
			else
			{
				base.RefreshTile(location, tileMap);
			}
		}

		public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, ref Matrix4x4 transform)
		{
			// Check rule against rotations of 0, 90, 180, 270
			for (int angle = 0; angle <= (rule.mRuleTransform == TilingRule.Transform.Rotated ? 270 : 0); angle += 90)
			{
				if (RuleMatches(rule, position, tilemap, angle))
				{
					transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -angle), Vector3.one);
					return true;
				}
			}

			// Check rule against x-axis mirror
			if ((rule.mRuleTransform == TilingRule.Transform.MirrorX) && RuleMatches(rule, position, tilemap, true, false))
			{
				transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, 1f, 1f));
				return true;
			}

			// Check rule against y-axis mirror
			if ((rule.mRuleTransform == TilingRule.Transform.MirrorY) && RuleMatches(rule, position, tilemap, false, true))
			{
				transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, -1f, 1f));
				return true;
			}

			return false;
		}

		private static Matrix4x4 ApplyRandomTransform(TilingRule.Transform type, Matrix4x4 original, float perlinScale, Vector3Int position)
		{
			float perlin = GetPerlinValue(position, perlinScale, 200000f);
			switch (type)
			{
				case TilingRule.Transform.MirrorX:
					return original * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(perlin < 0.5 ? 1f : -1f, 1f, 1f));
				case TilingRule.Transform.MirrorY:
					return original * Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, perlin < 0.5 ? 1f : -1f, 1f));
				case TilingRule.Transform.Rotated:
					int angle = Mathf.Clamp(Mathf.FloorToInt(perlin * 4), 0, 3) * 90;
					return Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -angle), Vector3.one);		
			}
			return original;
		}

		public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, int angle)
		{
			for (int y = -1; y <= 1; y++)
			{
				for (int x = -1; x <= 1; x++)
				{
					if (x != 0 || y != 0)
					{
						Vector3Int offset = new Vector3Int(x, y, 0);
						Vector3Int rotated = GetRotatedPos(offset, angle);
						int index = GetIndexOfOffset(rotated);
						TileBase tile = tilemap.GetTile(position + offset);
						if (rule.mNeighbors[index] == TilingRule.Neighbor.This && tile != this || rule.mNeighbors[index] == TilingRule.Neighbor.NotThis && tile == this)
						{
							return false;
						}	
					}
				}
				
			}
			return true;
		}

		public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, bool mirrorX, bool mirrorY)
		{
			for (int y = -1; y <= 1; y++)
			{
				for (int x = -1; x <= 1; x++)
				{
					if (x != 0 || y != 0)
					{
						Vector3Int offset = new Vector3Int(x, y, 0);
						Vector3Int mirrored = GetMirroredPos(offset, mirrorX, mirrorY);
						int index = GetIndexOfOffset(mirrored);
						TileBase tile = tilemap.GetTile(position + offset);
						if (rule.mNeighbors[index] == TilingRule.Neighbor.This && tile != this || rule.mNeighbors[index] == TilingRule.Neighbor.NotThis && tile == this)
						{
							return false;
						}
					}
				}
			}
			
			return true;
		}

		private int GetIndexOfOffset(Vector3Int offset)
		{
			int result = offset.x + 1 + (-offset.y + 1) * 3;
			if (result >= 4)
				result--;
			return result;
		}

		public Vector3Int GetRotatedPos(Vector3Int original, int rotation)
		{
			switch (rotation)
			{
				case 0:
					return original;
				case 90:
					return new Vector3Int(-original.y, original.x, original.z);
				case 180:
					return new Vector3Int(-original.x, -original.y, original.z);
				case 270:
					return new Vector3Int(original.y, -original.x, original.z);
			}
			return original;
		}

		public Vector3Int GetMirroredPos(Vector3Int original, bool mirrorX, bool mirrorY)
		{
			return new Vector3Int(original.x * (mirrorX ? -1 : 1), original.y * (mirrorY ? -1 : 1), original.z);
		}
	}
}
