using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage;
using Windows.AI.MachineLearning;
namespace GamingEmotes
{
    
    public sealed class Input
    {
        public TensorFloat Input2505; // shape(1,1,64,64)
    }
    
    public sealed class Output
    {
        public TensorFloat Softmax2997_Output_0; // shape(1,8)
    }
    
    public sealed class Model
    {
        private LearningModel model;
        private LearningModelSession session;
        private LearningModelBinding binding;
        public static Model CreateFromFilePath(string path)
        {
            Model learningModel = new Model();
            learningModel.model = LearningModel.LoadFromFilePath(path);
            learningModel.session = new LearningModelSession(learningModel.model);
            learningModel.binding = new LearningModelBinding(learningModel.session);
            return learningModel;
        }
        public async Task<Output> Evaluate(Input input)
        {
            binding.Bind("Input2505", input.Input2505);
            var result = await session.EvaluateAsync(binding, "0");
            var output = new Output();
            output.Softmax2997_Output_0 = result.Outputs["Softmax2997_Output_0"] as TensorFloat;
            return output;
        }
    }
}
