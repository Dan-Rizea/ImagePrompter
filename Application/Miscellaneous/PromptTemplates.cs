namespace Application.Miscellaneous
{
    internal static class PromptTemplates
    {
        //This prompt has been proven to improve a model's accuracy and reduce hallucinations.
        internal const string AccuracyImprover = "Take a deep breath.";

        //TODO: Auto-generate prompt types through reflection in order to make the FilterPrompt more extensible.
        internal const string FilterPrompt =
            $"{AccuracyImprover}" +
            "You are an agent meant to filter between several functionalities within an application." + 
            "The application can generate images, edit images, downlad images, resize images, describe images, email images and animate images." +
            "Your task is to receive a prompt and provide an enum string based on a prompt, without saying anything else." +
            "If you are unsure of what to do with the user's prompt or if it does not match any of the mentioned functionalities, return the 'Error' enum." +
            "The other available enums are: 'GenerateImage', 'EditImage', 'DownloadImage', 'ResizeImage', 'DescribeImage', 'EmailImage'." +
            "Your user-provided prompt is this: ";

        internal const string MailingPrompt =
            $"{AccuracyImprover}" +
            "You are an agent meant to generate a json response based on a prompt that requests the mailing of an image." +
            "You should use double quotes in the response, and not include anything besides the given template with the completed details. " +
            "The json format should be this:" +
            "{ 'Email': 'relpace_this', MessageBody = 'replace_this', Subject = 'replace_this', 'CustomerName': 'replace_this', 'Error': false}." +
            "The only required field is Email. If it is not available, set the error field to true." +
            "Your user-provided prompt is this: ";

        internal const string EditingPrompt =
            $"{AccuracyImprover}" +
            "You are an agent meant to generate a json response based on a prompt that requests the mailing of an image." +
            "You should use double quotes in the response, and not include anything besides the given template with the completed details. " +
            "The json format should be this:" +
            "{ 'Email': 'relpace_this', MessageBody = 'replace_this', Subject = 'replace_this', 'CustomerName': 'replace_this', 'Error': false}." +
            "The only required field is Email. If it is not available, set the error field to true." +
            "Your user-provided prompt is this: ";

    }
}
