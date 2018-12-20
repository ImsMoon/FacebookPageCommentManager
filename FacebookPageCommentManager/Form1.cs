using Facebook;
using FacebookPageCommentManager.DataModels;
using FacebookPageCommentManager.MongoDbAccess;
using FacebookPageCommentManager.Properties;
using FacebookPageMessegingApp;
using FacebookPageMessegingApp.DataModels;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace FacebookPageCommentManager
{
    public partial class Form1 : Form
    {
        private string Profile_Access_token = null;
        private const string AppId = "176295779966540";
        private const string ExtendedPermissions = "read_page_mailboxes";
        private string _accessToken;
        private string profilename = null;
        private readonly DataGridViewLinkColumn btn = new DataGridViewLinkColumn();
        List<FbPage> PageList = new List<FbPage>();
        List<PostListView> loadedpostlist = new List<PostListView>();
        DateTime CompleteLoadingTime;
        DataTable Pagedt = new DataTable();

        List<FbPage> postlist = new List<FbPage>();
        private Dictionary<string, string> PostwiseMesaages = new Dictionary<string, string>();

        string currPagename;
        string currPageid;
        string currPageAccessToken;
   
        private string User_Profile_Link = "";

        List<string> cnnectionStatus = new List<string>();


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            checkedListBox1.DataSource = null;
        }

        

        private void btnFBLogin_Click(object sender, EventArgs e)
        {
            progressBar1.Show();
            label1.Text = "Facebook login";
            if (btnFBLogin.Text == "Logout")
            {
                checkedListBox1.DataSource = null;
                AutoLikeBox.Clear();
               
                btnFBLogin.Text = "Login";
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                try
                {
                    PageList = null;
                }catch
                { }
                User_Profile_Link = "";
                Profile_Access_token = null;
                //logout cache clear
                var fb = new FacebookClient();
                var logoutUrl = fb.GetLogoutUrl(new
                {
                    next = "https://www.facebook.com/connect/login_success.html",
                    access_token = _accessToken
                });
                var webBrowser = new WebBrowser();
                //webBrowser.Navigated += (o, args) =>
                //{
                //    if (args.Url.AbsoluteUri == "https://www.facebook.com/connect/login_success.html")
                //       this.Close();
                //};
                webBrowser.Navigate(logoutUrl.AbsoluteUri);
            }
            else
            {
                var FacebookLogin = new FacebookLoginDialog(AppId, ExtendedPermissions);
                FacebookLogin.ShowDialog();
                FbLoginFunction(FacebookLogin.FacebookOAuthResult);
            }
            progressBar1.Hide();
            label1.Text = "";
        }

        private void FbLoginFunction(FacebookOAuthResult facebookOAuthResult)
        {
            if (facebookOAuthResult != null)
            {
                if (facebookOAuthResult.IsSuccess)
                {
                    _accessToken = facebookOAuthResult.AccessToken;
                    
                    Profile_Access_token = _accessToken;
                    var fb = new FacebookClient(facebookOAuthResult.AccessToken);

                    dynamic result = fb.Get("/me");
                    var name = result.name;
                    linkLabel1.Text = name;
                   
                    linkLabel1.Show();

                    dynamic profileLink = fb.Get("/me?fields=link");
                    User_Profile_Link = "www.facebook.com";

                    //

                    // to get page list with id and name
                    dynamic pages = fb.Get("/me?fields=accounts{name,id}");

                    FbPage pg1 = new FbPage();
                   
                   
                    if (pages.accounts != null)
                    {
                        var PageNameFromAccount = pages.accounts.data;
                        for (int i = 0; i < 5; i++)
                        {
                            try
                            {
                                FbPage pg = new FbPage();
                                pg.Id = PageNameFromAccount[i].id;
                                pg.Name = PageNameFromAccount[i].name;
                                PageList.Add(pg);
                            }
                            catch (Exception)
                            {
                                break;
                            }
                        }
                    }



                    /// var PageNameFromData = PageNameFromAccount[0].name;
                    
                    checkedListBox1.DataSource = PageList;
                    checkedListBox1.ValueMember = "Id";
                    checkedListBox1.DisplayMember = "Name";
                    //checkedListBox1.SelectedIndex = -1;
                    btnFBLogin.Text = "Logout";
                }
                else
                {
                    MessageBox.Show(facebookOAuthResult.ErrorDescription);
                }
            }
        }

        private async Task<List<PostListView>> getpostlist(string CurrentpageAccesstoken,string SelectedPageId,string CurrentPageName,string rule)
        {
            if (rule != "Timer")
            {
                progressBar1.Show();
                label1.Text = "Loading..";
            }
            List<PostListView> listofposts = new List<PostListView>();
            List<PostListView> AllConversationViewList = new List<PostListView>();
            //start getting all conversation list of selected page
            var FbClient = new FacebookClient();
            FbClient.AccessToken = CurrentpageAccesstoken;
            dynamic AllPostJson;
            try
            {
                AllPostJson = FbClient.Get(SelectedPageId + "?fields=posts");
            }catch(Exception ex)
            {
                if (labelStatus.Text == "Status: Connected")
                {
                    labelStatus.Text = DateTime.Now + "   EROROR MESSAGE: " + ex.Message;
                    labelStatus.ForeColor = Color.Red;
                    cnnectionStatus.Add(labelStatus.Text);
                }
                return null;
            }
            var AllPostData = AllPostJson.posts.data;

            if (rule == "Timer")
            {
                
            }
            else
            {
                foreach (var item in AllPostData)
                {
                    FbPage post = new FbPage();
                    post.Id = item.id;
                    post.Name = item.message;
                    postlist.Add(post);
                }
            }

            foreach (var posts in AllPostData)
            {
                dynamic SinglePostComments;
                try
                {
                    if (rule == "Timer")
                    {
                        SinglePostComments = await FbClient.GetTaskAsync(posts.id + "/comments?fields=is_hidden,from,permalink_url,message,comment_count,private_reply_conversation,can_reply_privately,created_time&summary=1&order=reverse_chronological&limit=2");
                    }
                    else
                    {
                        SinglePostComments = await FbClient.GetTaskAsync(posts.id + "/comments?fields=is_hidden,from,permalink_url,message,comment_count,private_reply_conversation,can_reply_privately,created_time&summary=1&order=reverse_chronological&limit=8");
                    }
                }
                catch (Exception x)
                {
                    if (labelStatus.Text == "Status: Connected")
                    {
                        labelStatus.Text = DateTime.Now + "   EROROR MESSAGE: " + x.Message;
                        labelStatus.ForeColor = Color.Red;
                        cnnectionStatus.Add(labelStatus.Text);
                    }
                    return null;
                }
                try
                {

                    var postcomments = SinglePostComments.data;

                    foreach (var comments in postcomments)
                    {
                        PostListView postdata = new PostListView();
                        postdata.postname = posts.message;
                        postdata.pagename = CurrentPageName;
                        postdata.createdtime = DateTime.Parse(comments.created_time);
                        try
                        {
                            postdata.fromname = comments.from.name;
                        }
                        catch (Exception)
                        {
                            postdata.fromname = "";
                        }
                        postdata.postid = comments.id.ToString();
                        postdata.replies = comments.message;
                        try
                        {
                            postdata.postlink = comments.permalink_url;
                        }
                        catch (Exception)
                        {
                            postdata.postlink = "www.facebook.com/" + CurrentPageName + "/posts/" + posts.id;
                        }
                        postdata.ishidden = comments.is_hidden.ToString();
                        postdata.repliescount = comments.comment_count.ToString();
                        postdata.canreply = comments.can_reply_privately.ToString();
                        try
                        {
                            postdata.inbox = "www.facebook.com/" + comments.private_reply_conversation.link;
                        }
                        catch (Exception)
                        {
                            postdata.inbox = "";
                        }

                        listofposts.Add(postdata);

                        if (rule != "Timer")
                        {
                            label1.Text = listofposts.Count + " Loading...";
                        }
                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }
            progressBar1.Hide();
            labelStatus.Text = "Status: Connected";
            labelStatus.ForeColor = Color.Lime;
            return listofposts;
        }

        public void datagridload(List<PostListView> allcomments)
        {
            progressBar1.Show();
            dataGridView1.DataSource = null;

            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();
            Pagedt = new DataTable();

            
            Pagedt.Columns.Add("Serial");//0
            Pagedt.Columns.Add("Page Name");//1
            Pagedt.Columns.Add("Post Name");//2
            Pagedt.Columns.Add("Message");//3
            Pagedt.Columns.Add("From Name");//4
            Pagedt.Columns.Add("Created Time");//5
            Pagedt.Columns.Add("Replies");//6
            Pagedt.Columns.Add("Is Hidden");//7
            Pagedt.Columns.Add("Can Send Message?");//8
            Pagedt.Columns.Add("Inbox");//9
            Pagedt.Columns.Add("Postid");//10
            Pagedt.Columns.Add("CommentLink");//11
          
            int serial = allcomments.Count;
            foreach (var item in allcomments)
            {
                 Pagedt.Rows.Add(serial,item.pagename,item.postname,item.replies,item.fromname,item.createdtime,
                     item.repliescount,item.ishidden,item.canreply,item.inbox,item.postid,item.postlink);
                //btn.Text = serial.ToString();
                serial--;
            }
            
            dataGridView1.DataSource = Pagedt;

            dataGridView1.Columns["Postid"].Visible = false;
            dataGridView1.Columns["CommentLink"].Visible = false;
            dataGridView1.Columns["Inbox"].DefaultCellStyle.ForeColor = Color.Blue;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;
            
            int i = 0, gridviewrowno = dataGridView1.Rows.Count;
            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //link creation
                DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                updatecell.Value = allcomments[i].postlink;
                dataGridView1.Rows[i].Cells[5] = updatecell;
                DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                inboxcell.Value = allcomments[i].inbox;
                dataGridView1.Rows[i].Cells[9] = inboxcell;

                DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                if (allcomments[i].ishidden == "True")
                    ishidden.Value = true;
                else
                {
                    ishidden.Value = false;
                }

                dataGridView1.Rows[i].Cells[7] = ishidden;

                DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                if (allcomments[i].canreply == "True")
                    canreply.Value = canreply.FalseValue;
                else
                {
                    canreply.Value = canreply.TrueValue;
                }

                dataGridView1.Rows[i].Cells[8] = canreply;

                progressBar1.Hide();
            }

        }


        private async void checkedListBox1_SelectedIndexChangedAsync(object sender, EventArgs e)
        {
            if (checkedListBox1.GetItemCheckState(checkedListBox1.SelectedIndex) == CheckState.Checked)
            {
                Cursor.Current = Cursors.WaitCursor;
                progressBar1.Show();
                var fb = new FacebookClient();
                fb.AccessToken = Profile_Access_token;
                AutoLikeBox.Clear();
                try
                {
                    //start getting selected page access token
                    string CurrentPageId = checkedListBox1.SelectedValue.ToString();
                    string CurrentPageName = checkedListBox1.Text;// get the selected text from checklistbox

                    currPagename = CurrentPageName;
                    currPageid = CurrentPageId;
                   
                    if (CurrentPageId != "0")
                    {
                        dynamic CurrentPageTokenCall = fb.Get(CurrentPageId + "?fields=access_token");
                        string CurrentpageAccesstoken = CurrentPageTokenCall.access_token;
                        currPageAccessToken = CurrentpageAccesstoken;
                        List<PostListView> AllConversationViewList = new List<PostListView>();
                        AllConversationViewList =await getpostlist(CurrentpageAccesstoken,CurrentPageId, CurrentPageName,"");
                        AllConversationViewList.Sort((y, x) => -1 * DateTime.Compare(y.createdtime, x.createdtime));
                       
                        label1.Text = AllConversationViewList.Count + " New Comments";
                        datagridload(AllConversationViewList);
                        loadedpostlist = AllConversationViewList;///
                        CompleteLoadingTime = AllConversationViewList[0].createdtime;
                        //. for loading post list in listbox

                        try
                        {
                            PostlistBox.DataSource = postlist;
                            PostlistBox.ValueMember = "id";
                            PostlistBox.DisplayMember = "Name";
                            PostlistBox.SelectedIndex = -1;
                        }catch(Exception)
                        { }
                        /// end section
                       
                    }
                    else
                    {
                        AutoLikeBox.Clear();

                        dataGridView1.DataSource = null;
                        dataGridView1.Rows.Clear();
                        dataGridView1.Refresh();
                    }
                }

                catch (Exception)
                {

                }
                try
                {
                    string path = Assembly.GetExecutingAssembly().Location;
                    path = Path.GetDirectoryName(path);
                    path = Path.Combine(path, "notifyTune.wav");
                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);

                    //player.SoundLocation = @"C:\Users\imtiyaz\Downloads\Music\notifyTune.wav";
                    player.Play();
                }
                catch
                {

                }
            }
            Cursor.Current = Cursors.Default;
            progressBar1.Hide();
        }

        private void commentsScraperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (btnFBLogin.Text != "Logout")
            {
                MessageBox.Show("Please Login first");
            }
            else
            {
                CommentsScrapperForm csf = new CommentsScrapperForm(Profile_Access_token,PageList);
                csf.Show();
            }
        }

        
        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(textBox8.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Message LIKE '%{0}%' ", textBox8.Text);
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        //link creation
                        DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                        updatecell.Value = loadedpostlist[i].postlink;
                        dataGridView1.Rows[i].Cells[5] = updatecell;
                        DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                        inboxcell.Value = loadedpostlist[i].inbox;
                        dataGridView1.Rows[i].Cells[9] = inboxcell;



                        DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                        if (loadedpostlist[i].ishidden == "True")
                            ishidden.Value = true;
                        else
                        {
                            ishidden.Value = false;
                        }

                        dataGridView1.Rows[i].Cells[7] = ishidden;

                        DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                        if (loadedpostlist[i].canreply == "True")
                            canreply.Value = canreply.FalseValue;
                        else
                        {
                            canreply.Value = canreply.TrueValue;
                        }

                        dataGridView1.Rows[i].Cells[8] = canreply;

                        progressBar1.Hide();
                    }
                }
                else
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Empty;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        //link creation
                        DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                        updatecell.Value = loadedpostlist[i].postlink;
                        dataGridView1.Rows[i].Cells[5] = updatecell;
                        DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                        inboxcell.Value = loadedpostlist[i].inbox;
                        dataGridView1.Rows[i].Cells[9] = inboxcell;



                        DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                        if (loadedpostlist[i].ishidden == "True")
                            ishidden.Value = true;
                        else
                        {
                            ishidden.Value = false;
                        }

                        dataGridView1.Rows[i].Cells[7] = ishidden;

                        DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                        if (loadedpostlist[i].canreply == "True")
                            canreply.Value = canreply.FalseValue;
                        else
                        {
                            canreply.Value = canreply.TrueValue;
                        }

                        dataGridView1.Rows[i].Cells[8] = canreply;

                        progressBar1.Hide();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void btnRun_Click(object sender, EventArgs e)
        {
            if (loadedpostlist.Count > 0)
            {
                if (btnRun.Text == "Stop")
                {
                    timer1.Stop();
                    btnRun.Text = "Run";
                    btnRun.BackColor = Color.PaleGreen;
                    AutoSendMessageBox.Enabled = true;
                    AutoBlockBox.Enabled = true;
                    AutoDeleteBox.Enabled = true;
                    AutoHideBox.Enabled = true;
                    AutoReplyBox.Enabled = true;
                    AutoLikeBox.Enabled = true;
                    HideAttachmentCheckBox.Enabled = true;
                    HideLinkCheckBox.Enabled = true;
                }
                else
                {
                    timer1.Start();
                    btnRun.BackColor = Color.PaleVioletRed;
                    btnRun.Text = "Stop";
                    AutoSendMessageBox.Enabled = false;
                    AutoBlockBox.Enabled = false;
                    AutoDeleteBox.Enabled = false;
                    AutoHideBox.Enabled = false;
                    AutoReplyBox.Enabled = false;
                    AutoLikeBox.Enabled = false;
                    HideAttachmentCheckBox.Enabled = false;
                    HideLinkCheckBox.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please load comment list first");
            }

        }


        private async void timer1_TickAsync(object sender, EventArgs e)
        {
            if (loadedpostlist.Count >0)
            {
                Regex rx = new Regex(@"([(http|ftp|https):\/\/][\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?)");
                timer1.Stop();
                List<PostListView> isupdate = new List<PostListView>();
                
                try
                {
                    isupdate = await getpostlist(currPageAccessToken, currPageid, currPagename, "Timer");
                    isupdate.Sort((y, x) => -1 * DateTime.Compare(y.createdtime, x.createdtime));

                    if (isupdate[0].createdtime > CompleteLoadingTime)
                    {
                        foreach (var data in isupdate)
                        {
                            if (data.createdtime > CompleteLoadingTime && data.postid != dataGridView1.Rows[0].Cells[10].Value.ToString())
                            {
                                DataRow newRow = Pagedt.NewRow();
                                newRow["Serial"] = dataGridView1.Rows.Count+1;
                                newRow["Page Name"] = data.pagename;
                                newRow["Post Name"] = data.postname;
                                newRow["Message"] = data.replies;
                                newRow["From Name"] = data.fromname;
                                newRow["Created Time"] = data.createdtime;
                                newRow["Replies"] = data.repliescount;
                                newRow["Is Hidden"] = data.ishidden;
                                newRow["Can Send Message?"] = data.canreply;
                                newRow["Inbox"] = data.inbox;
                                newRow["Postid"] = data.postid;
                                newRow["CommentLink"] = data.postlink;

                                Pagedt.Rows.InsertAt(newRow,0);
                              
                                dataGridView1.Rows[0].DefaultCellStyle.Font = new Font(DataGridView.DefaultFont, FontStyle.Bold);
                                //Pagedt.Rows.Add(dataGridView1.Rows.Count, data.pagename, data.postname, data.replies,
                                //    data.fromname, data.createdtime, data.repliescount, data.ishidden, data.canreply, data.inbox, data.postid, data.postlink);

                                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                                {
                                    try
                                    {
                                        //link creation
                                        DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                                        updatecell.Value = loadedpostlist[i].postlink;
                                        dataGridView1.Rows[i].Cells[5] = updatecell;
                                        DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                                        inboxcell.Value = loadedpostlist[i].inbox;
                                        dataGridView1.Rows[i].Cells[9] = inboxcell;



                                        DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                                        if (loadedpostlist[i].ishidden == "True")
                                            ishidden.Value = true;
                                        else
                                        {
                                            ishidden.Value = false;
                                        }

                                        dataGridView1.Rows[i].Cells[7] = ishidden;

                                        DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                                        if (loadedpostlist[i].canreply == "True")
                                            canreply.Value = canreply.FalseValue;
                                        else
                                        {
                                            canreply.Value = canreply.TrueValue;
                                        }

                                        dataGridView1.Rows[i].Cells[8] = canreply;
                                    }catch(Exception ex)
                                    {
                                        //MessageBox.Show(ex.Message);
                                        continue;
                                    }

                                }

                                var fb = new FacebookClient
                                {
                                    AccessToken = currPageAccessToken
                                };

                                if (!string.IsNullOrEmpty(AutoLikeBox.Text)|| AutoLikeBox.Text == "*")
                                {
                                    
                                    
                                    try
                                    {
                                       
                                            try
                                            {
                                                fb.Post(data.postid + "/likes", null);
                                            }
                                            catch (Exception)
                                            {                                                
                                                continue;
                                            }
                                        
                                    }catch
                                    {

                                    }
                                  }

                                if (!string.IsNullOrEmpty(AutoReplyBox.Text))
                                {
                                   
                                    try{
                                        string message = AutoReplyBox.Text;
                                        
                                            try
                                            {
                                                var msgbody = new Dictionary<string, object>
                                                    {
                                                        { "message",message}
                                                    };
                                                fb.Post(data.postid + "/comments", msgbody);
                                            }
                                            catch (Exception)
                                            {
                                                continue;
                                            }
                                        
                                    }
                                    catch
                                    {

                                    }
                                    Thread.Sleep(500);
                                }

                                if (!string.IsNullOrEmpty(AutoHideBox.Text) || HideAttachmentCheckBox.Checked || HideLinkCheckBox.Checked)
                                {
                                    try
                                    {
                                          
                                        string message = AutoHideBox.Text;
                                        if (data.replies.Contains(message) || data.replies == "" || rx.IsMatch(data.replies))
                                        {                                            
                                                try
                                                {
                                                    bool ishidden = true;
                                                    var msgbody = new Dictionary<string, object>
                                                        {
                                                            {"is_hidden",ishidden}
                                                        };

                                                    fb.Post(data.postid, msgbody);
                                                }
                                                catch (Exception)
                                                {
                                                    continue;
                                                }
                                            
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    Thread.Sleep(500);
                                }

                                if (!string.IsNullOrEmpty(AutoDeleteBox.Text))
                                {
                                    try
                                    {
                                        string message = AutoDeleteBox.Text;
                                        if (data.replies.Contains(message))
                                        {
                                            
                                                try
                                                {
                                                    fb.Delete(data.postid);
                                                }
                                                catch (Exception)
                                                {
                                                    continue;
                                                }
                                            
                                        }
                                    }
                                    catch
                                    {

                                    }
                                    Thread.Sleep(500);
                                }

                                if (PostwiseMesaages.Count > 0 )
                                {
                                    try
                                    {
                                        string message = "";
                                        if (PostwiseMesaages.ContainsKey(data.postname))
                                        {
                                           PostwiseMesaages.TryGetValue(data.postname, out message);
                                        }
                                        else
                                        {
                                            message = null;
                                        }
                                        try
                                        {
                                            if (data.canreply == "True" && !string.IsNullOrWhiteSpace(message))
                                            {
                                                progressBar1.Show();

                                                var msgbody = new Dictionary<string, object>
                                                    {
                                                        { "message",message}
                                                    };
                                                await fb.PostTaskAsync(data.postid + "/private_replies", msgbody);
                                                progressBar1.Hide();
                                                Thread.Sleep(3000);

                                            }else
                                            {
                                                //MessageBox.Show("Sorry can not send message to " + dataGridView1.Rows.Count);
                                                dataGridView1.Rows[dataGridView1.Rows.Count].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                                            }
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }                                        
                                    }
                                    catch
                                    {

                                    }
                                }

                                try
                                {
                                    PopupNotifier popup = new PopupNotifier();                                    
                                    popup.TitleText = " New Comment found.....";
                                    popup.ContentText = " Commnet: " + data.replies;
                                    popup.Popup();
                                }catch(Exception)
                                {

                                }

                                try
                                {
                                    string path = Assembly.GetExecutingAssembly().Location;
                                    path = Path.GetDirectoryName(path);
                                    path = Path.Combine(path, "bird.wav");
                                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);

                                    //player.SoundLocation = @"C:\Users\imtiyaz\Downloads\Music\notifyTune.wav";
                                    player.Play();
                                }
                                catch
                                {

                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                        CompleteLoadingTime = isupdate[0].createdtime;
                    }else
                    {
                    }
                    
                }
                catch (Exception)
                {
                    // MessageBox.Show(ex.Message);
                    Thread.Sleep(500);
                }
            }
            
            timer1.Start();
        }

       


        private void likeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Show();
            var fb = new FacebookClient();
            fb.AccessToken = currPageAccessToken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[10].Value.ToString();
                        fb.Post(id + "/likes", null);
                    }
                    catch (Exception)
                    {
                        countFailure++;
                        continue;
                    }
                }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + " comments like Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            progressBar1.Hide();
        }

        private void replyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Show();
            var fb = new FacebookClient();
            fb.AccessToken = currPageAccessToken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[10].Value.ToString();
                        bool ishidden = true;
                        var msgbody = new Dictionary<string, object>
                            {
                                {"is_hidden",ishidden}
                            };

                        fb.Post(id, msgbody);
                    }
                    catch (Exception)
                    {
                        countFailure++;
                        continue;
                    }
                }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + " comments Hide Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            progressBar1.Show();
        }

        private void unhideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            fb.AccessToken = currPageAccessToken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[10].Value.ToString();
                        bool ishidden = false;
                        var msgbody = new Dictionary<string, object>
                        {
                            {"is_hidden",ishidden}
                        };

                        fb.Post(id, msgbody);
                    }
                    catch (Exception)
                    {
                        countFailure++;
                        continue;
                    }
                }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + " comments UnHide Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static string ShowDialog(string name, string title, string txtboxtxt)
        {
            Form prompt = new Form();
            prompt.MaximizeBox = false;
            prompt.MinimizeBox = false;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;

            prompt.Width = 441;
            prompt.Height = 276;
            prompt.Text = title;

            Button ok = new Button() { Text = "Ok", Left = 338, Top = 16 };
            Button clear = new Button() { Text = "Clear", Left = 338, Top = 45 };


            prompt.Controls.Add(ok);
            prompt.Controls.Add(clear);

            Label textLabel = new Label() { Left = 50, Top = 20, Text = name };
            TextBox inputBox = new TextBox() { Left = 20, Top = 75, Width = 390, Height = 147 };
            inputBox.Multiline = true;
            inputBox.Text = txtboxtxt;

            ok.Click += (sender, e) => { prompt.Close(); };
            clear.Click += (sender, e) => { inputBox.Clear(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.ShowDialog();
            return (string)inputBox.Text.ToString();
        }


        private async void sendMessageToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            progressBar1.Show();
            string replydata = ShowDialog("Please Enter Your Reply", " Reply", "");
            var fb = new FacebookClient();
            fb.AccessToken = currPageAccessToken;
            int sentcount = 1;
            int index = 0;
            int countFailure = 0;
            try
            {
                string[] sendFailure = new string[dataGridView1.SelectedRows.Count];
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[10].Value.ToString();
                        string cansendmsg = dataGridView1.Rows[index].Cells[8].Value.ToString();
                        if (cansendmsg == "True")
                        {
                            var msgbody = new Dictionary<string, object>
                                {
                                    { "message",replydata}
                                };
                            await fb.PostTaskAsync(id + "/private_replies", msgbody);
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Aqua;
                            // totalCommentLabel.Text = sentcount + "/" + dataGridView1.SelectedRows.Count + " Sending...";
                            sentcount++;
                            Thread.Sleep(4000);
                        }else
                        {
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                            sendFailure[countFailure] = dataGridView1.Rows[index].Cells[0].Value.ToString();
                            countFailure++;
                            Thread.Sleep(4000);
                        }
                    }
                    catch (Exception)
                    {
                        dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Red;
                        sendFailure[countFailure] = dataGridView1.Rows[index].Cells[0].Value.ToString();
                        countFailure++;
                        Thread.Sleep(4000);
                        continue;
                    }

                }
                string path = Assembly.GetExecutingAssembly().Location;
                path = Path.GetDirectoryName(path);
                path = Path.Combine(path, "notifyTune.wav");
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);

                //player.SoundLocation = @"C:\Users\imtiyaz\Downloads\Music\notifyTune.wav";
                player.Play();
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + "  Sends Message Successfully");
                if (sendFailure.Length > 1)
                {
                    string toDisplay = string.Join(Environment.NewLine, sendFailure);
                    MessageBox.Show("Can not sent messages to these Users" + "\n\n" + toDisplay);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           // totalCommentLabel.Text = loadedCommentsCount + " Comments Found";
            progressBar1.Hide();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            fb.AccessToken = currPageAccessToken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[10].Value.ToString();
                        fb.Delete(id);
                    }
                    catch (Exception)
                    {
                        countFailure++;
                        continue;
                    }
                }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + "  comments deleted Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (Profile_Access_token != null)
            {
                //Cursor.Current = Cursors.WaitCursor;
                var fb = new FacebookClient();
                fb.AccessToken = Profile_Access_token;


                if (currPageAccessToken != "0" && Profile_Access_token != null)
                {
                    if (e.ColumnIndex == 5)
                    {
                        dynamic CurrentPageTokenCall = fb.Get(currPageid + "?fields=access_token");

                        //on click redirect to browser inbox

                        try
                        {
                            string msg = dataGridView1.CurrentRow.Cells[11].Value.ToString();
                            System.Diagnostics.Process.Start(msg);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    if (e.ColumnIndex == 9)
                    {
                        dynamic CurrentPageTokenCall = fb.Get(currPageid + "?fields=access_token");

                        //on click redirect to browser inbox

                        try
                        {
                            string msg = dataGridView1.CurrentRow.Cells[9].Value.ToString();
                            System.Diagnostics.Process.Start(msg);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Please Login First.");
                }
            }
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            for  (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //link creation
                DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                updatecell.Value = loadedpostlist[i].postlink;
                dataGridView1.Rows[i].Cells[5] = updatecell;
                DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                inboxcell.Value = loadedpostlist[i].inbox;
                dataGridView1.Rows[i].Cells[9] = inboxcell;



                DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                if (loadedpostlist[i].ishidden == "True")
                    ishidden.Value = true;
                else
                {
                    ishidden.Value = false;
                }

                dataGridView1.Rows[i].Cells[7] = ishidden;

                DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                if (loadedpostlist[i].canreply == "True")
                    canreply.Value = canreply.FalseValue;
                else
                {
                    canreply.Value = canreply.TrueValue;
                }

                dataGridView1.Rows[i].Cells[8] = canreply;

                progressBar1.Hide();
            }

        }

        // dictionary for saving post wise messages
        

        private void PostlistBox_SelectedIndexChanged(object sender, EventArgs e)
        {
           String Text = (sender as ListBox).Text.ToString();
            ToolTip tip = new ToolTip
            {
                AutoPopDelay = 10000
            };
            tip.SetToolTip(PostlistBox,Text);

            //string postid = PostlistBox.SelectedValue.ToString();
            List<PostMsg> ListOfPostMsg = PostMsgDbAccess.GetMsg();
            PostwiseMesaages = ListOfPostMsg.ToDictionary(x => x.PostId, x => x.Message);

            if (PostwiseMesaages.Count > 0)
            {
                PostwiseMesaages.TryGetValue(Text, out string messages);
                AutoSendMessageBox.Text = messages;
            }

        }

        private void btnAddMessage_Click(object sender, EventArgs e)
        {
            //string postid = PostlistBox.SelectedValue.ToString();

            if (PostwiseMesaages.ContainsKey(PostlistBox.Text))// checking either key exist or not
            {
                PostMsg pm = new PostMsg
                {
                    PostId = PostlistBox.Text,
                    Message = AutoSendMessageBox.Text
                };
                PostMsgDbAccess.Update(pm);
                PostwiseMesaages[PostlistBox.Text] = AutoSendMessageBox.Text;
            }else
            {
                PostMsg pm = new PostMsg
                {
                    PostId = PostlistBox.Text,
                    Message = AutoSendMessageBox.Text
                };
                PostMsgDbAccess.Create(pm);
                PostwiseMesaages.Add(PostlistBox.Text, AutoSendMessageBox.Text);
            }
        }

        private void labelStatus_Click(object sender, EventArgs e)
        {
            ConnectionMonitor cm = new ConnectionMonitor(cnnectionStatus);
            cm.Show();
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }
    }
} 
