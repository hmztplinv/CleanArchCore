using Microsoft.AspNetCore.Mvc;

public class CustomValidationProblemDetails:ProblemDetails
{
   public IDictionary<string,string[]> Errors { get; set; }=new Dictionary<string, string[]>();
}