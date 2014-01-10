using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Searching Code/Files
    /// http://developer.github.com/v3/search/#search-code
    /// </summary>
    public class SearchCodeRequest : BaseSearchRequest
    {
        public SearchCodeRequest(string term)
            :base(term)
        {
        }

        private string sortQuery;

        /// <summary>
        /// Optional Sort field. Can only be indexed, which indicates how recently a file has been indexed by the 
        /// GitHub search infrastructure. If not provided, results are sorted by best match.
        /// </summary>
        public override string Sort
        {
            get
            {
                return sortQuery;
            }
            set
            {
                sortQuery = value == "indexed" ? "indexed" : "";
            }
        }

        /// <summary>
        /// https://help.github.com/articles/searching-code#language
        /// </summary>
        public Language? Language { get; set; }

        public override string MergedQualifiers()
        {
            var parameters = new List<string>();

            if (Language != null)
            {
                parameters.Add(String.Format(CultureInfo.InvariantCulture, "language:{0}", Language));
            }

            return String.Join("+", parameters);
        }
    }
}
