namespace ITHSCourseSchool.Models
{
    public class StringRouteConstraint : IRouteConstraint
    {

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            object value;
            if (values.TryGetValue(routeKey, out value) && value is string)
            {
                // Validate the value of the route parameter here and return true if it is valid
                return true;
            }

            return false;
        }



    }
}
