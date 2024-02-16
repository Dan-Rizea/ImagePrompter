namespace Application.Miscellaneous
{
    internal static class PromptTemplates
    {
        //This prompt has been proven to improve a model's accuracy and reduce hallucinations.
        internal const string AccuracyImprover = "Take a deep breath.";

        //TODO: Auto-generate prompt types through reflection in order to make the FilterPrompt more extensible.
        internal const string FilterPrompt =
            $@"{AccuracyImprover}
            You are an agent meant to filter between several functionalities within an application. 
            The application can generate images, edit images, downlad images, resize images, describe images, email images and animate images.
            Your task is to receive a prompt and provide an enum string based on a prompt, without saying anything else.
            If you are unsure of what to do with the user's prompt, return the 'Error' enum.
            The other available enums are: 'GenerateImage', 'EditImage', 'DownloadImage', 'ResizeImage', 'DescribeImage', 'EmailImage', 'AnimateImage'.
            Your user-provided prompt is this: ";
    }
}
