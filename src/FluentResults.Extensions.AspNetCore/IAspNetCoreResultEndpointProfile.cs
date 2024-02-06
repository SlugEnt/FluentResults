using SlugEnt.FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace SlugEnt.FluentResults.Extensions.AspNetCore
{
    public interface IAspNetCoreResultEndpointProfile
    {
        ActionResult TransformFailedResultToActionResult(FailedResultToActionResultTransformationContext context);

        ActionResult TransformOkNoValueResultToActionResult(OkResultToActionResultTransformationContext<Result> context);

        ActionResult TransformOkValueResultToActionResult<T>(OkResultToActionResultTransformationContext<Result<T>> context);
    }
}