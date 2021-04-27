namespace GameCore
{
    public class Tools
    {
        static public string SqueezeString(string input)
        {
            int firstIndex = 0;
            int lastIndex = 0;
            int numberOfChars = input.Length;
            int offSet;
            while (firstIndex < numberOfChars && input[firstIndex] == ' ' || input[firstIndex] == '\n' || input[firstIndex] == '\t')
                firstIndex += 1;
            while (lastIndex > firstIndex && input[lastIndex] == ' ' || input[lastIndex] == '\n' || input[lastIndex] == '\t')
                lastIndex -= 1;
            lastIndex += 1;
            string shortenedString = "";
            for (int i = firstIndex; i < lastIndex; i++)
                shortenedString += input[i];
            return shortenedString;
        }
    }
}