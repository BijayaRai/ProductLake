using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ProductLake.HealthCheck
{
    public class ProductDataStoreHealthCheck : IHealthCheck
    {
        public static string Name => "ProductDataStoreHealthCheck";

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
             //If the procut data was store in a database, the following checks could be made:
             // - establish a connection
             // - execution a query to ensure that all tables are present.
            return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy));
        }
    }
}
