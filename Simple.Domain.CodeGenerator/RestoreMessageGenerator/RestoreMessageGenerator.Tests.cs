//TODO: extract to standalone project to avoid compilation errors.

// using Microsoft.VisualStudio.TestTools.UnitTesting;

// namespace Simple.Domain.CodeGenerator;

// partial class CodeGeneratorTests
// {
//   [TestMethod]
//   public void class_implementing_ievent__generate_restore_message_method__restore_message_with_class_payload_branch ()
//   {
//     Compilation inputCompilation = CreateCompilation(@"
// namespace Simple.Domain.Models;

// public interface IEvent;
// public record TestEvent: IEvent;
//     ");

//     var methodGenerator = new RestoreMessageGenerator();
//     var csharpDriver = CSharpGeneratorDriver.Create(methodGenerator);
//     var driver = csharpDriver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var _, out var _);

//     var runResult = driver.GetRunResult();
//     var generatorResult = runResult.Results[0];
//     var actual = generatorResult.GeneratedSources[0].SourceText.ToString();

//     Console.WriteLine(actual);
//     StringAssert.Contains(actual, "nameof(TestEvent) => RestoreMessage(message, DeserializePayload<TestEvent>(message.MessageContent)!),", StringComparison.InvariantCulture);
//   }

//   [TestMethod]
//   public void class_implementing_inotification__generate_restore_message_method__restore_message_with_class_payload_branch ()
//   {
//     Compilation inputCompilation = CreateCompilation(@"
// namespace Simple.Domain.Models;

// public interface INotification;
// public record TestNotification: INotification;
//     ");

//     var methodGenerator = new RestoreMessageGenerator();
//     var csharpDriver = CSharpGeneratorDriver.Create(methodGenerator);
//     var driver = csharpDriver.RunGeneratorsAndUpdateCompilation(inputCompilation, out var _, out var _);

//     var runResult = driver.GetRunResult();
//     var generatorResult = runResult.Results[0];
//     var actual = generatorResult.GeneratedSources[0].SourceText.ToString();

//     StringAssert.Contains(actual, "nameof(TestNotification) => RestoreMessage(message, DeserializePayload<TestNotification>(message.MessageContent)!),", StringComparison.InvariantCulture);
//   }
// }