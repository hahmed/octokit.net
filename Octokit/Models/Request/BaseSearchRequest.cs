using Octokit.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octokit
{
    /// <summary>
    /// Base class for searching issues/code/users/repos
    /// </summary>
    public abstract class BaseSearchRequest
    {
        public BaseSearchRequest(string term)
        {
            Ensure.ArgumentNotNullOrEmptyString(term, "term");
            Term = term;
            Page = 1;
            PerPage = 100;
            Order = SortDirection.Descending;
        }

        /// <summary>
        /// The search term
        /// </summary>
        public string Term { get; private set; }

        /// <summary>
        /// The sort field
        /// </summary>
        public override string Sort
        {
            get;
            set;
        }

        private string SortOrder
        {
            get
            {
                return Order == SortDirection.Ascending ? "asc" : "desc";
            }
        }

        /// <summary>
        /// Optional Sort order if sort parameter is provided. One of asc or desc; the default is desc.
        /// </summary>
        public SortDirection Order { get; set; }

        /// <summary>
        /// Page of paginated results
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of items per page
        /// </summary>
        public int PerPage { get; set; }

        /// <summary>
        /// All qualifiers are merged into one string
        /// </summary>
        public abstract string MergedQualifiers();

        /// <summary>
        /// Get the query parameters that will be appending onto the search
        /// </summary>
        public System.Collections.Generic.IDictionary<string, string> Parameters
        {
            get
            {
                var d = new System.Collections.Generic.Dictionary<string, string>();
                d.Add("page", Page.ToString());
                d.Add("per_page", PerPage.ToString());
                d.Add("sort", Sort);
                d.Add("order", SortOrder);
                var mergedQualifiers = MergedQualifiers();
                d.Add("q", Term + (mergedQualifiers.IsNotBlank() ? "+" + mergedQualifiers : "")); //add qualifiers onto the search term
                return d;
            }
        }
    }
}
