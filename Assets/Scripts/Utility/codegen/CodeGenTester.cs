using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Codegen;

public class CodeGenTester : MonoBehaviour {
    public void CodeBlockTest() {
        int max = 11;

        CodeBlock block = CodeBlock.NewBuilder()
                                   .AddStatement("int sum = 0")
                                   .BeginControlFlow("for(int i = 0; i <= $A; i++)", max)
                                   .AddStatement("sum += i")
                                   .BeginControlFlow("if(sum > $A && i % 2 == $A", max, 0)
                                   .AddStatement("sum++")
                                   .NextControlFlow("else")
                                   .AddStatement("sum--")
                                   .EndControlFlow()
                                   .EndControlFlow()
                                   .Build();
			
        Debug.Log(block);
			
        CodeBlock block2 = CodeBlock.NewBuilder()
                                    .AddStatement("int sum = 0")
                                    .BeginControlFlow("do")
                                    .AddStatement("sum++")
                                    .EndControlFlow("while(sum < $A)", max)
                                    .Build();
			
        Debug.Log(block2);
    }

    public void FieldBlockTest() {
	    FieldBlock field = FieldBlock.NewBuilder("field")
	                                 .AddModifiers(CodeBuilderUtil.Modifier.PRIVATE, CodeBuilderUtil.Modifier.READONLY)
	                                 .AddType(typeof(FieldBlock))
	                                 .Build();
	    Debug.Log(field);
    }

    public void MethodBlockTest() {
	    int max = 11;
	    
	    CodeBlock block = CodeBlock.NewBuilder()
	                               .AddStatement("int sum = 0")
	                               .BeginControlFlow("for(int i = 0; i <= $A; i++)", max)
	                               .AddStatement("sum += i")
	                               .BeginControlFlow("if(sum > $A && i % 2 == $A", max, 0)
	                               .AddStatement("sum++")
	                               .NextControlFlow("else")
	                               .AddStatement("sum--")
	                               .EndControlFlow()
	                               .EndControlFlow()
	                               .Build();

	    MethodBlock method = MethodBlock.NewBuilder("Testing")
	                                    .AddModifiers(CodeBuilderUtil.Modifier.PUBLIC, CodeBuilderUtil.Modifier.STATIC)
	                                    .AddCodeBlock(block)
	                                    .Build();
	    
	    Debug.Log(method);
    }
}
