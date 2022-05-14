using DiffFinder.Models;
using System.Text;

namespace DiffFinder.Business
{
    public class DiffFinderService
    {
        public bool IsSameSize(string leftString, string rightString)
        {
            byte[] leftData = Encoding.UTF8.GetBytes(leftString);
            byte[] rightData = Encoding.UTF8.GetBytes(rightString);

            if (leftData.Length != rightData.Length)
            {
                return false;
            }
            return true;
        }

        public Response CalculateDifference(DiffrenceInformation diffrenceInformation)
        {
            string leftString = diffrenceInformation.LeftString.Trim();
            string rightString = diffrenceInformation.RightString.Trim();
            Response response = new Response();

            if (!IsSameSize(leftString, rightString))
            {
                response.ResultMessage = "Inputs are of different size.";
                response.DiffsOffsetList = new List<DiffsOffsets>();
                return response;
            }
            List<DiffsOffsets> diffOffsetList = new List<DiffsOffsets>();
            for (int i = 0; i < leftString.Length; i++)
            {
                int diffCount = 0;
                byte[] byteListLeftStringChar = Encoding.UTF8.GetBytes(leftString[i].ToString());
                byte[] byteListRightStringChar = Encoding.UTF8.GetBytes(rightString[i].ToString());
                for (int j = 0; j < byteListLeftStringChar.Length && j < byteListRightStringChar.Length; j++)
                {
                    if (byteListLeftStringChar[j] != byteListRightStringChar[j])
                    {
                        diffCount++;
                    }
                }
                if (diffCount != 0)
                {
                    DiffsOffsets diffsOffsets = new DiffsOffsets();
                    diffsOffsets.Offset = i;
                    diffsOffsets.Diffs = diffCount;
                    diffOffsetList.Add(diffsOffsets);
                }


                response.DiffsOffsetList = diffOffsetList;
            }

            if (diffOffsetList.Count == 0)
            {
                response.ResultMessage = "Inputs were equal.";
            }
            else
            {
                response.ResultMessage = "Offsets and lenghts of the diffences.";
            }
            return response;
        }
    }

    public class Response
    {
        public string ResultMessage { get; set; }
        public List<DiffsOffsets> DiffsOffsetList { get; set; }
    }
}
