using System.Web;
using System.Web.Optimization;

namespace datareporter
{
    public class BundleConfig
    {
        // 有关捆绑的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // 使用要用于开发和学习的 Modernizr 的开发版本。然后，当你做好
            // 生产准备就绪，请使用 https://modernizr.com 上的生成工具仅选择所需的测试。
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/iframeResizer.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/css/css.css",
                "~/Content/css/css_0.css",
                "~/Content/css/cssmain.css",
                "~/Content/css/admincss/skin.css"));

            bundles.Add(new StyleBundle("~/datatable/css").Include
               ("~/Scripts/bootstrap-v3.3.7/css/bootstrap.min.css"
               , "~/Scripts/datatables-v1.10.18/css/dataTables.bootstrap.min.css"));


            bundles.Add(new ScriptBundle("~/datatable/js").Include
                ("~/Scripts/jquery-v3.3.1/jquery-3.3.1.min.js"
               , "~/Scripts/datatables-v1.10.18/js/jquery.dataTables.min.js"
               , "~/Scripts/datatables-v1.10.18/js/dataTables.bootstrap.min.js"));
        }

    }
}
