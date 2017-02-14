﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NJsonSchema.CodeGeneration.CSharp;

namespace NJsonSchema.CodeGeneration.Tests.CSharp
{
    [TestClass]
    public class CSharpEnumGeneratorTests
    {
        [TestMethod]
        public async Task When_enum_has_no_type_then_enum_is_generated()
        {
            //// Arrange
            var json =
            @"{
                ""type"": ""object"", 
                ""properties"": {
                    ""category"" : {
                        ""enum"" : [
                            ""commercial"",
                            ""residential""
                        ]
                    }
                }
            }";
            var schema = await JsonSchema4.FromJsonAsync(json);
            var generator = new CSharpGenerator(schema);

            //// Act
            var code = generator.GenerateFile("MyClass");

            //// Assert
            Assert.IsTrue(code.Contains("public enum MyClassCategory"));
        }

        [TestMethod]
        public async Task When_enum_name_contains_colon_then_it_is_removed_and_next_word_converted_to_upper_case()
        {
            //// Arrange
            var json = @"
            {
                ""type"": ""object"",
                ""properties"": {
                    ""event"": {
                        ""type"": ""string"",
                        ""enum"": [
                        ""pullrequest:updated"",
                        ""repo:commit_status_created"",
                        ""repo:updated"",
                        ""issue:comment_created"",
                        ""project:updated"",
                        ""pullrequest:rejected"",
                        ""pullrequest:fulfilled"",
                        ""repo:imported"",
                        ""repo:deleted"",
                        ""pullrequest:comment_created"",
                        ""pullrequest:comment_deleted"",
                        ""repo:fork"",
                        ""issue:created"",
                        ""repo:commit_comment_created"",
                        ""pullrequest:approved"",
                        ""repo:commit_status_updated"",
                        ""pullrequest:comment_updated"",
                        ""issue:updated"",
                        ""pullrequest:unapproved"",
                        ""pullrequest:created"",
                        ""repo:push""
                        ],
                        ""description"": ""The event identifier.""
                    }
                }
            }";

            var schema = await JsonSchema4.FromJsonAsync(json);
            var generator = new CSharpGenerator(schema);

            //// Act
            var code = generator.GenerateFile("MyClass");

            //// Assert
            Assert.IsTrue(code.Contains("PullrequestUpdated = 0,"));
        }
    }
}