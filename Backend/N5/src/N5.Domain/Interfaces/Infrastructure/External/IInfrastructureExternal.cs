namespace MillionAndUp.Archiecture.Domain.Interfaces.Infrastructure.External
{
    /// <summary>
    /// Base implementation Infrastructure
    /// </summary>
    /// <typeparam name="T">Request Message</typeparam>
    /// <typeparam name="Q">Response Message</typeparam>
    public interface IInfrastructureExternal<T, Q>
        where T : class
        where Q : class
    {
        /// <summary>
        /// Execute call exteral system
        /// </summary>
        /// <param name="request">Request Message</param>
        /// <returns>Response Message after call</returns>
        Task<Q> Execute(T request);
    }
}