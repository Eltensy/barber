using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;

namespace BarberLayered.Tests.Helpers;

public static class ControllerTestHelpers
{
    public static IHttpContextAccessor CreateHttpContextAccessor(ISession? session = null)
    {
        var httpContext = new DefaultHttpContext();

        if (session is not null)
        {
            httpContext.Features.Set<ISessionFeature>(new TestSessionFeature(session));
        }

        return new HttpContextAccessor
        {
            HttpContext = httpContext
        };
    }

    public static ITempDataDictionary CreateTempData()
    {
        var httpContext = new DefaultHttpContext();

        return new TempDataDictionary(
            httpContext,
            Mock.Of<ITempDataProvider>());
    }

    private sealed class TestSessionFeature : ISessionFeature
    {
        public TestSessionFeature(ISession session)
        {
            Session = session;
        }

        public ISession Session { get; set; }
    }
}