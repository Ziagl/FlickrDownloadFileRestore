namespace FlickrRestore;

internal class Metadata
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public string count_views { get; set; }
    public string count_faves { get; set; }
    public string count_comments { get; set; }
    public string date_taken { get; set; }
    public string count_tags { get; set; }
    public string count_notes { get; set; }
    public int rotation { get; set; }
    public string date_imported { get; set; }
    public string photopage { get; set; }
    public string original { get; set; }
    public string license { get; set; }
    public List<object> geo { get; set; }
    public List<object> groups { get; set; }
    public List<object> albums { get; set; }
    public List<object> tags { get; set; }
    public List<object> people { get; set; }
    public List<object> notes { get; set; }
    public string privacy { get; set; }
    public string comment_permissions { get; set; }
    public string tagging_permissions { get; set; }
    public string safety { get; set; }
    public List<object> comments { get; set; }
}
