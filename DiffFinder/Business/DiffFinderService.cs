using DiffFinder.Models;
using System.Text;

namespace DiffFinder.Business
{
    public class DiffFinderService
    {
        //this function checks both string is same size. If their size is not equal, return false. 
        public bool IsSameSize(string leftString, string rightString)
        {
            byte[] leftData = Convert.FromBase64String(leftString);
            byte[] rightData = Convert.FromBase64String(rightString);

            if (leftData.Length != rightData.Length)
            {
                return false;
            }
            return true;
        }
        //this function calculate the difference and offset values. 
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
                
                for (int j = 0; j < byteListLeftStringChar.Length; j++)
                {
                    var diff = (byteListLeftStringChar[j] ^ byteListRightStringChar[j]);
                    diffCount = 0;
                    while(diff > 0)
                    {
                        diffCount++;
                        diff &= (diff - 1);
                    }
                }
                if (diffCount != 0)
                {
                    DiffsOffsets diffsOffsets = new DiffsOffsets();
                    diffsOffsets.Offset = i;
                    diffsOffsets.Diffs = diffCount;
                    diffsOffsets.LeftChar = leftString[i].ToString();
                    diffsOffsets.RightChar = rightString[i].ToString();
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
    //this class for return the result.
    public class Response
    {
        public string ResultMessage { get; set; }
        public List<DiffsOffsets> DiffsOffsetList { get; set; }
    }
}
