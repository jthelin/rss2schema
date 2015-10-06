using System;
using System.Xml.Serialization;
using System.Globalization;

namespace rss2sample
{
    using rss2.schema;

    /// <summary>
    /// An example program showing how to use the code generated from the RSS 2.0 schema by xsd.exe
    /// </summary>
    class Program
    {
        static string weblogTitle = "TheArchitect.co.uk - Jorgen Thelin's weblog";
        static Uri weblogUri = new Uri("http://www.thearchitect.co.uk/weblog/");
        static Uri weblogImageUri = new Uri("http://www.thearchitect.co.uk/images/jorgen-thelin.jpg");
        static int weblogImageWidth = 125;
        static int weblogImageHeight = 100;
        static CultureInfo weblogLanguage = CultureInfo.GetCultureInfo("en-US");
        static string weblogRating = "(PICS-1.1 \"http://www.rsac.org/ratingsv01.html\" l by \"webmaster@example.com\" on \"2007.01.29T10:09-0800\" r (n 0 s 0 v 0 l 0))";

        static Uri itemIdTag = new Uri("tag:www.thearchitect.co.uk,2008:/weblog//2.520");
        static Uri itemUri = new Uri("http://www.thearchitect.co.uk/weblog/archives/2008/09/spore_arrives_drm_copyprotection_is_a_bug_not_a_feature.html");
        static string itemTitle = "Spore Arrives";
        static string itemBody = "The much anticipated Spore game is available today";
        static string itemCategoryName = "Games";
        static DateTime itemPubDate = new DateTime(2008, 9, 7, 20, 2, 1, DateTimeKind.Utc);

        static CultureInfo RssLocFormat = CultureInfo.GetCultureInfo("en-US");

        static void Main()
        {
            rss rss = new rss();
            RssChannel rssChannel = new RssChannel();
            rss.channel = rssChannel;

            Image feedImage = new Image();
            feedImage.link = weblogUri.AbsoluteUri;
            feedImage.title = weblogTitle;
            feedImage.width = weblogImageWidth.ToString(RssLocFormat);
            feedImage.height = weblogImageHeight.ToString(RssLocFormat);
            feedImage.url = weblogImageUri.AbsoluteUri;

            Guid itemGuid = new Guid();
            itemGuid.isPermaLink = false;
            itemGuid.Value = itemIdTag.AbsoluteUri;

            Category itemCategory = new Category();
            itemCategory.Value = itemCategoryName;

            RssItem rssItem = new RssItem();
            rssItem.ItemsElementName = new ItemsChoiceType1[] {
                // Ordering must correspond to the contents of rssItem.Items
                ItemsChoiceType1.link,
                ItemsChoiceType1.pubDate,
                ItemsChoiceType1.guid,
                ItemsChoiceType1.title,
                ItemsChoiceType1.description,
                ItemsChoiceType1.category
            };
            rssItem.Items = new object[] {
                itemUri.AbsoluteUri,  // Must be a string value
                itemPubDate.ToString(  // Format data value
                    DateTimeFormatInfo.InvariantInfo.RFC1123Pattern,
                    RssLocFormat ),  
                itemGuid,
                itemTitle,
                itemBody,
                itemCategory
            };

            rssChannel.ItemsElementName = new ItemsChoiceType[] {
                // Ordering must correspond to the contents of rssChannel.Items
                ItemsChoiceType.title,
                ItemsChoiceType.link,
                ItemsChoiceType.language,
                ItemsChoiceType.image,
                ItemsChoiceType.rating
            };
            rssChannel.Items = new object[] {
                weblogTitle,
                weblogUri.AbsoluteUri,  // Must be a string value
                weblogLanguage.ToString(), // Must be a string value
                feedImage,
                weblogRating
            };
            // Add the blog post item(s)
            rssChannel.item = new RssItem[] { 
                rssItem 
            };

            XmlSerializer serializer = new XmlSerializer(typeof(rss));
            serializer.Serialize(Console.Out, rss);

            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadLine();
        }
    }
}

// Sample Output:
//
//<?xml version="1.0" encoding="IBM437"?>
//<rss 
// xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" 
// xmlns:xsd="http://www.w3.org/2001/XMLSchema" 
// version="2.0">
//  <channel>
//    <title>TheArchitect.co.uk - Jorgen Thelin's weblog</title>
//    <link>http://www.thearchitect.co.uk/weblog/</link>
//    <language>en-US</language>
//    <image>
//      <url>http://www.thearchitect.co.uk/images/jorgen-thelin.jpg</url>
//      <link>http://www.thearchitect.co.uk/weblog/</link>
//      <width>125</width>
//      <height>100</height>
//      <title>TheArchitect.co.uk - Jorgen Thelin's weblog</title>
//    </image>
//    <rating>
//      (PICS-1.1 "http://www.rsac.org/ratingsv01.html" l by "webmaster@example.com" on "2007.01.29T10:09-0800" r (n 0 s 0 v 0 l 0))
//    </rating>
//    <item>
//      <link>http://www.thearchitect.co.uk/weblog/archives/2008/09/spore_arrives_drm_copyprotection_is_a_bug_not_a_feature.html</link>
//      <pubDate>Sun, 07 Sep 2008 20:02:01 GMT</pubDate>
//      <guid isPermaLink="false">tag:www.thearchitect.co.uk,2008:/weblog//2.520</guid>
//      <title>Spore Arrives</title>
//      <description>The much anticipated Spore game is available today</description>
//      <category>Games</category>
//    </item>
//  </channel>
//</rss>
