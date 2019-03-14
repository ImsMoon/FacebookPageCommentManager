using Facebook;
using FacebookPageCommentManager.DataModels;
using FacebookPageMessegingApp.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FacebookPageCommentManager
{
    public partial class CommentsScrapperForm : Form
    {
        List<FbPage> pages = new List<FbPage>();
        string Profile_Access_token;
        string CurrentPageId;
        string CurrentpageAccesstoken;
        string postid;// object id....
        List<CommentsScrapperView> loadedComments = new List<CommentsScrapperView>();

        int loadedCommentsCount = 0;
        int updateCommentsCount = 0;
        public CommentsScrapperForm(string ProfileAccesstoken,List<FbPage> PageList)
        {
            InitializeComponent();
            Profile_Access_token = ProfileAccesstoken;
            pages = PageList;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CommentsScrapperForm_Load(object sender, EventArgs e)
        {
            
            fbpagelist.DataSource = pages;
            fbpagelist.ValueMember = "Id";
            fbpagelist.DisplayMember = "Name";
            fbpagelist.SelectedItem = null;
            fbpagelist.SelectedText = "Select Page...";
        }

        public int getupdate(List<string> updateCommnetList)
        {
            int updateVallue = 0;



            return updateVallue;
        }

       public async Task<List<CommentsScrapperView>> getcommentslistAsync(string currentpageaccesstoken, string currentpageid)
        {
            progressBar1.Show();

            List<CommentsScrapperView> commentslist = new List<CommentsScrapperView>();
            var FbClient = new FacebookClient();
            FbClient.AccessToken = currentpageaccesstoken;

            string textboxdata = postidlOrlinkText.Text;
           
            if(textboxdata.Contains('_') == true && textboxdata.Contains("facebook.com") == false)
            {
                postid = textboxdata;
            }else if(textboxdata.Contains("facebook.com") == true)
            {               
               var posturl = new Uri(textboxdata).Segments;
               string partialid = posturl[posturl.Length - 1];
               postid = currentpageid + '_' + partialid;
            }else
            {
                string partialid = textboxdata;
                postid = currentpageid + '_' + partialid;
            }
            try
            {
                dynamic SinglePostComments = await FbClient.GetTaskAsync(postid + "?fields=story,message,comments.limit(1000){comment_count,is_hidden,created_time,from,message,id,permalink_url,private_reply_conversation,can_reply_privately,comments{comment_count,is_hidden,created_time,from,message,id,permalink_url,private_reply_conversation,user_likes,like_count,can_reply_privately},user_likes,like_count}");
                var postcomments = SinglePostComments.comments.data;

                foreach(var comments in postcomments)
                {
                    progressBar1.Show();

                    CommentsScrapperView cm = new CommentsScrapperView();
                    cm.postid = comments.id.ToString();
                    cm.createdtime = comments.created_time.ToString();
                    cm.testDate =Convert.ToDateTime(comments.created_time);

                    try
                    {
                        cm.fromname = comments.from.name;
                    }catch(Exception)
                    {
                        cm.fromname = "";
                    }
                    try
                    {
                        cm.fromid = comments.from.id.ToString();
                    }
                    catch (Exception)
                    {
                        cm.fromid = "";
                    }
                    cm.postname = SinglePostComments.message;
                    cm.message = comments.message;
                    cm.replies = comments.comment_count.ToString();
                    cm.postlink = comments.permalink_url;
                    cm.likes = comments.like_count.ToString();
                    cm.islike = comments.user_likes.ToString();
                    cm.ishidden = comments.is_hidden.ToString();
                    cm.canreply = comments.can_reply_privately.ToString();
                    try
                    {
                        cm.inbox = "www.facebook.com/" + comments.private_reply_conversation.link;
                    }
                    catch (Exception)
                    {
                        cm.inbox = "";
                    }
                    commentslist.Add(cm);
                    if (comments.comment_count > 0)
                    {
                        progressBar1.Show();

                        for (int replycount = 0; replycount <= comments.comment_count - 1; replycount++)
                        {
                            try
                            {
                                progressBar1.Show();

                                totalCommentLabel.Text = (commentslist.Count + replycount).ToString()+ "  Commnets Loading...";
                                var postcommentsreply = comments.comments.data[replycount];
                                CommentsScrapperView postreply = new CommentsScrapperView();

                                postreply.postname = SinglePostComments.message;
                                postreply.postid = postcommentsreply.id;
                                postreply.createdtime = postcommentsreply.created_time.ToString();
                                postreply.testDate = Convert.ToDateTime(postcommentsreply.created_time);
                                try
                                {
                                    postreply.fromname = postcommentsreply.from.name;
                                }
                                catch (Exception)
                                {
                                    postreply.fromname = "";
                                }
                                try
                                {
                                    postreply.fromid = postcommentsreply.from.id.ToString();
                                }
                                catch (Exception)
                                {
                                    postreply.fromid = "";
                                }
                                postreply.message = postcommentsreply.message;
                                postreply.postlink = postcommentsreply.permalink_url;
                                try
                                {
                                    postreply.inbox = "www.facebook.com/" + postcommentsreply.private_reply_conversation.link;
                                }
                                catch (Exception)
                                {
                                    postreply.inbox = "";
                                }
                                postreply.ishidden = postcommentsreply.is_hidden.ToString();
                                postreply.likes = postcommentsreply.like_count.ToString();
                                postreply.islike = postcommentsreply.user_likes.ToString();
                                postreply.canreply = postcommentsreply.can_reply_privately.ToString();
                                postreply.replies = "0";

                                commentslist.Add(postreply);
                            }
                            catch (Exception)
                            {
                                continue;
                            }
                        }
                    }
                }
                try
                {
                    var next = SinglePostComments.comments.paging.next;
                    dynamic nextdata = await FbClient.GetTaskAsync(next);
                    progressBar1.Show();

                    var newnext = nextdata.data;
                    for (int nextcount = 0; nextcount <= 50; nextcount++)
                    {
                        try
                        {
                            progressBar1.Show();

                            if (nextcount > 0)
                            {

                                next = nextdata.paging.next;
                                nextdata = await FbClient.GetTaskAsync(next);
                                newnext = "";
                                newnext = nextdata.data;
                            }
                            foreach (var comments in newnext)
                            {
                                progressBar1.Show();

                                CommentsScrapperView cm = new CommentsScrapperView();
                                cm.postid = comments.id.ToString();
                                cm.createdtime = comments.created_time.ToString();
                                cm.testDate = Convert.ToDateTime(comments.created_time);
                                try
                                {
                                    cm.fromname = comments.from.name;
                                }
                                catch (Exception)
                                {
                                    cm.fromname = "";
                                }
                                try
                                {
                                    cm.fromid = comments.from.id.ToString();
                                }
                                catch (Exception)
                                {
                                    cm.fromid = "";
                                }
                                cm.postname = SinglePostComments.message;
                                cm.message = comments.message;
                                cm.replies = comments.comment_count.ToString();
                                cm.postlink = comments.permalink_url;
                                cm.likes = comments.like_count.ToString();
                                cm.islike = comments.user_likes.ToString();
                                cm.ishidden = comments.is_hidden.ToString();
                                cm.canreply = comments.can_reply_privately.ToString();
                                try
                                {
                                    cm.inbox = "www.facebook.com/" + comments.private_reply_conversation.link;
                                }
                                catch (Exception)
                                {
                                    cm.inbox = "";
                                }
                                commentslist.Add(cm);
                                if (comments.comment_count > 0)
                                {
                                    progressBar1.Show();

                                    for (int replycount = 0; replycount <= comments.comment_count - 1; replycount++)
                                    {
                                        try
                                        {
                                            progressBar1.Show();

                                            totalCommentLabel.Text = (commentslist.Count + replycount).ToString() + "  Commnets Loading...";
                                            var postcommentsreply = comments.comments.data[replycount];
                                            CommentsScrapperView postreply = new CommentsScrapperView();

                                            postreply.postname = SinglePostComments.message;
                                            postreply.postid = postcommentsreply.id;
                                            postreply.createdtime = postcommentsreply.created_time.ToString();
                                            postreply.testDate = postcommentsreply.created_time;
                                            try
                                            {
                                                postreply.fromname = postcommentsreply.from.name;
                                            }
                                            catch (Exception)
                                            {
                                                postreply.fromname = "";
                                            }
                                            try
                                            {
                                                postreply.fromid = postcommentsreply.from.id.ToString();
                                            }
                                            catch (Exception)
                                            {
                                                postreply.fromid = "";
                                            }
                                            postreply.message = postcommentsreply.message;
                                            postreply.postlink = postcommentsreply.permalink_url;
                                            try
                                            {
                                                postreply.inbox = "www.facebook.com/" + postcommentsreply.private_reply_conversation.link;
                                            }
                                            catch (Exception)
                                            {
                                                postreply.inbox = "";
                                            }
                                            postreply.ishidden = postcommentsreply.is_hidden.ToString();
                                            postreply.likes = postcommentsreply.like_count.ToString();
                                            postreply.islike = postcommentsreply.user_likes.ToString();
                                            postreply.canreply = postcommentsreply.can_reply_privately.ToString();
                                            postreply.replies = "0";

                                            commentslist.Add(postreply);
                                        }
                                        catch (Exception)
                                        {
                                            continue;
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        {
                            break;
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Invalid Post Id / Link"+ex.Message);
            }

            loadedCommentsCount = commentslist.Count;
            progressBar1.Hide();
            return commentslist; 
        }

        private void fbpagelist_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            progressBar1.Show();

            var fb = new FacebookClient();
            fb.AccessToken = Profile_Access_token;
            postidlOrlinkText.Clear();
            try
            {
                //start getting selected page access token
                CurrentPageId = fbpagelist.SelectedValue.ToString();
                if (CurrentPageId != "0")
                {
                    dynamic CurrentPageTokenCall = fb.Get(CurrentPageId + "?fields=access_token");
                    CurrentpageAccesstoken = CurrentPageTokenCall.access_token;                   
                }
                else
                {
                    postidlOrlinkText.Clear();
                    dataGridView1.DataSource = null;
                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();
                }
            }
            catch (Exception)
            {

            }
            Cursor.Current = Cursors.Default;
            progressBar1.Hide();
        }

        public void loaddatagrid (List<CommentsScrapperView> allcomments)
        {
            progressBar1.Show();
            dataGridView1.DataSource = null;

            dataGridView1.Columns.Clear();
            dataGridView1.Refresh();

            DataTable Pagedt = new DataTable();
            Pagedt.Columns.Add("Serial");//0
            Pagedt.Columns.Add("Post Name");//1
            Pagedt.Columns.Add("Message");//2
            Pagedt.Columns.Add("From Name");//3
            Pagedt.Columns.Add("Created Time");//4
            Pagedt.Columns.Add("Replies");//5
            Pagedt.Columns.Add("Likes");//6
            Pagedt.Columns.Add("User Like");//7
            Pagedt.Columns.Add("Is Hidden");//8
            Pagedt.Columns.Add("Can send message?");//9
            Pagedt.Columns.Add("Inbox");//10
            Pagedt.Columns.Add("Postid");//11
            Pagedt.Columns.Add("UserId");//12
            Pagedt.Columns.Add("CommentLink");//13



            // adddataRow(AllConversationViewList);
            loadedComments = allcomments;
            int serial = allcomments.Count;
            foreach (var item in allcomments)
            {
                Pagedt.Rows.Add(serial,item.postname,item.message,item.fromname,item.testDate,item.replies,item.likes,item.islike,
                    item.ishidden,item.canreply,item.inbox,item.postid,item.fromid,item.postlink);
                //btn.Text = serial.ToString();
                serial--;

            }
            dataGridView1.DataSource = Pagedt;

            dataGridView1.Columns["Postid"].Visible = false;
            dataGridView1.Columns["UserId"].Visible = false;
            dataGridView1.Columns["CommentLink"].Visible = false;
            dataGridView1.Columns["Inbox"].DefaultCellStyle.ForeColor = Color.Blue;

            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ReadOnly = true;


            
            //adddataRow(AllConversationViewList);
            int i = 0, gridviewrowno = dataGridView1.Rows.Count;
            for (i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //link creation
                DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                updatecell.Value = allcomments[i].postlink;
                dataGridView1.Rows[i].Cells[4] = updatecell;
                DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                inboxcell.Value = allcomments[i].inbox;
                dataGridView1.Rows[i].Cells[10] = inboxcell;

                DataGridViewCheckBoxCell islike = new DataGridViewCheckBoxCell();
                if (allcomments[i].islike == "True")
                    islike.Value = true;
                else
                {
                    islike.Value = false;
                }

                dataGridView1.Rows[i].Cells[7] = islike;

                DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                if (allcomments[i].ishidden == "True")
                    ishidden.Value = true;
                else
                {
                    ishidden.Value = false;
                }

                dataGridView1.Rows[i].Cells[8] = ishidden;

                DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                if (allcomments[i].canreply == "True")
                    canreply.Value = canreply.FalseValue;
                else
                {
                    canreply.Value = canreply.TrueValue;
                }

                dataGridView1.Rows[i].Cells[9] = canreply;

                progressBar1.Hide();
            }
        }

        private async void btnGetComments_ClickAsync(object sender, EventArgs e)
        {
            progressBar1.Show();
            if (!string.IsNullOrEmpty(postidlOrlinkText.Text))
            {
                progressBar1.Show();
                Cursor.Current = Cursors.WaitCursor;
                List<CommentsScrapperView> AllComments = new List<CommentsScrapperView>();
                AllComments = await getcommentslistAsync(CurrentpageAccesstoken, CurrentPageId);
                AllComments.Sort((y, x) => -1 * DateTime.Compare(y.testDate, x.testDate));
                //AllComments.OrderByDescending(x => x.testDate).ToList();
                loaddatagrid(AllComments);
                totalCommentLabel.Text = AllComments.Count.ToString() + " Comments Found";

                if (AllComments.Count > 0)
                {
                    try
                    {
                        string path = Assembly.GetExecutingAssembly().Location;
                        path = Path.GetDirectoryName(path);
                        path = Path.Combine(path, "notifyTune.wav");
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);

                        //player.SoundLocation = @"C:\Users\imtiyaz\Downloads\Music\notifyTune.wav";
                        player.Play();
                    }catch(Exception)
                    {

                    }

                    MessageBox.Show("Loading Complete " + AllComments.Count + " Comments found");
                }
                Cursor.Current = Cursors.Default;
                progressBar1.Hide();
            }else
            {
                MessageBox.Show("Please Insert Comment URL");
            }
            progressBar1.Hide();
        }

        private void btnReply_Click(object sender, EventArgs e)
        {
            if (btnReply.Text == "Stop")
            {
                btnReply.Text = "Replay";
            }else
            {
                btnReply.Text = "Stop";
                contextMenuStrip1.Show(Cursor.Position);
            }
        }

        public static Tuple<string,string> ShowDialog(string name, string title, string txtboxtxt)
        {
            Form prompt = new Form();
            prompt.MaximizeBox = false;
            prompt.MinimizeBox = false;
            prompt.StartPosition = FormStartPosition.CenterScreen;
            prompt.FormBorderStyle = FormBorderStyle.FixedDialog;

            prompt.Width = 441;
            prompt.Height = 300;
            prompt.Text = title;

            Button ok = new Button() { Text = "Ok", Left = 338, Top = 16 };
            Button clear = new Button() { Text = "Clear", Left = 338, Top = 45 };


            prompt.Controls.Add(ok);
            prompt.Controls.Add(clear);

            Label textLabel = new Label() { Left = 50, Top = 20, Text = name };
            TextBox inputBox = new TextBox() { Left = 20, Top = 75, Width = 390, Height = 147 };
            TextBox TimeInputBox = new TextBox() { Left = 20, Top = 225, Width = 70, Height = 14 };

            inputBox.Multiline = true;
            inputBox.Text = txtboxtxt;
            TimeInputBox.Text = "3";

            ok.Click += (sender, e) => { prompt.Close(); };
            clear.Click += (sender, e) => { inputBox.Clear(); };

            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(inputBox);
            prompt.Controls.Add(TimeInputBox);
            prompt.ShowDialog();
            var Tuple = new Tuple<string, string>(inputBox.Text, TimeInputBox.Text);
            return Tuple;
        }



        private void likeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            fb.AccessToken = CurrentpageAccesstoken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[11].Value.ToString();
                        fb.Post(id + "/likes", null);
                        Thread.Sleep(4000);
                    }catch(Exception)
                    {
                        countFailure++;
                        Thread.Sleep(4000);
                        continue;
                    }
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
                catch(Exception)
                {

                }
                MessageBox.Show(dataGridView1.SelectedRows.Count+" out of "+(dataGridView1.SelectedRows.Count-countFailure)+" comments like Successfully");
            }catch(Exception  ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ReplyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnReply.Text = "Stop";
            var replydata = ShowDialog("Please Enter Your Reply", " Reply", "");
            progressBar1.Show();
            if (replydata.Item1 != "")
            {
                var fb = new FacebookClient();
                fb.AccessToken = CurrentpageAccesstoken;
                int index = 0;
                int countFailure = 0;
                try
                {
                    for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                    {
                        if (btnReply.Text == "Reply")
                        {
                            break;
                        }
                        try
                        {
                            index = dataGridView1.SelectedRows[i].Index;
                            string id = dataGridView1.Rows[index].Cells[11].Value.ToString();
                            var msgbody = new Dictionary<string, object>
                            {
                                { "message",replydata.Item1}
                            };
                            var result = fb.Post(id + "/comments", msgbody);
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Aqua;

                            if (replydata.Item2 != "")
                                Thread.Sleep(Convert.ToInt16(replydata.Item2) * 1000);
                            else
                                Thread.Sleep(4000);
                        }
                        catch (Exception)
                        {
                            countFailure++;
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                            Thread.Sleep(4000);
                            continue;
                        }
                    }
                    try
                    {
                        // 5 nov 2018
                        string path = Assembly.GetExecutingAssembly().Location;
                        path = Path.GetDirectoryName(path);
                        path = Path.Combine(path, "notifyTune.wav");
                        System.Media.SoundPlayer player = new System.Media.SoundPlayer(path);

                        //player.SoundLocation = @"C:\Users\imtiyaz\Downloads\Music\notifyTune.wav";
                        player.Play();
                    }
                    catch (Exception)
                    {

                    }
                    MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + " comments replied Successfully");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Comments not send because " + ex.Message);
                }
            }
            progressBar1.Hide();
        }

        private void hideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            fb.AccessToken = CurrentpageAccesstoken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[11].Value.ToString();
                        bool ishidden = true;
                        var msgbody = new Dictionary<string, object>
                            {
                                {"is_hidden",ishidden}
                            };

                        fb.Post(id, msgbody);
                        dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Aqua;
                        Thread.Sleep(4000);

                    }
                    catch (Exception)
                    {
                        countFailure++;
                        Thread.Sleep(4000);
                        continue;
                    }
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
                catch(Exception)
                {

                }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + " comments Hide Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void unhideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            progressBar1.Show();
            var fb = new FacebookClient();
            fb.AccessToken = CurrentpageAccesstoken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[11].Value.ToString();
                        bool ishidden = false;
                        var msgbody = new Dictionary<string, object>
                        {
                            {"is_hidden",ishidden}
                        };

                        fb.Post(id, msgbody);
                        Thread.Sleep(4000);
                    }catch(Exception)
                    {
                        countFailure++;
                        Thread.Sleep(4000);
                        continue;
                    }
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
                catch(Exception)
                {

                }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + " comments UnHide Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            progressBar1.Hide();
        }

        private async void sendMessageToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            progressBar1.Show();
            var replydata = ShowDialog("Please Enter Your Message", " Message", "");
            if (replydata.Item1 != "")
            {
                var fb = new FacebookClient();
                fb.AccessToken = CurrentpageAccesstoken;
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
                            string id = dataGridView1.Rows[index].Cells[11].Value.ToString();
                            var msgbody = new Dictionary<string, object>
                            {
                                { "message",replydata.Item1}
                            };
                            await fb.PostTaskAsync(id + "/private_replies", msgbody);
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.Aqua;
                            totalCommentLabel.Text = sentcount + "/" + dataGridView1.SelectedRows.Count + " Sending...";
                            sentcount++;
                            if (replydata.Item2 != "")
                                Thread.Sleep(Convert.ToInt16(replydata.Item2)*1000);
                            else
                                Thread.Sleep(4000);
                        }
                        catch (Exception)
                        {
                            dataGridView1.Rows[index].DefaultCellStyle.BackColor = Color.PaleVioletRed;
                            sendFailure[countFailure] = dataGridView1.Rows[index].Cells[0].Value.ToString();
                            countFailure++;
                            Thread.Sleep(4000);
                            continue;
                        }

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
                    catch (Exception)
                    {

                    }
                    MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + "  Sends Message Successfully");
                    if (countFailure > 1)
                    {
                        string toDisplay = string.Join(Environment.NewLine, sendFailure);
                        MessageBox.Show("Can not sent messages to these Users" + "\n\n" + toDisplay);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                totalCommentLabel.Text = loadedCommentsCount + " Comments Found";
            }
            progressBar1.Hide();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            fb.AccessToken = CurrentpageAccesstoken;
            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[11].Value.ToString();
                        fb.Delete(id);
                    }
                    catch (Exception)
                    {
                        countFailure++;
                        continue;
                    }
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
                catch(Exception)
                {

                }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + "  comments deleted Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void banFromPageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fb = new FacebookClient();
            fb.AccessToken = CurrentpageAccesstoken;

            int index = 0;
            int countFailure = 0;
            try
            {
                for (int i = 0; i <= dataGridView1.SelectedRows.Count - 1; i++)
                {
                    try
                    {
                        index = dataGridView1.SelectedRows[i].Index;
                        string id = dataGridView1.Rows[index].Cells[11].Value.ToString();
                        var blockedId = new Dictionary<string, object>
                            {
                                {"user",id}
                            };

                        fb.Post(CurrentPageId + "/blocked", blockedId);
                    }catch(Exception)
                    {
                        countFailure++;
                        continue;
                    }
               }
                MessageBox.Show(dataGridView1.SelectedRows.Count + " out of " + (dataGridView1.SelectedRows.Count - countFailure) + "  user has been Blocked Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
           
                //Export data into file
                StringBuilder strB = new StringBuilder();
                //create html & table
                strB.AppendLine("<html><body><center><" +
                              "table border='1' cellpadding='0' cellspacing='0'>");
                strB.AppendLine("<tr>");
                //cteate table header
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    strB.AppendLine("<td align='center' valign='middle'>" +
                                   dataGridView1.Columns[i].HeaderText + "</td>");
                }
                //create table body
                strB.AppendLine("<tr>");
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    strB.AppendLine("<tr>");
                    foreach (DataGridViewCell dgvc in dataGridView1.Rows[i].Cells)
                    {
                        if (dgvc.Value != null)
                        {
                            strB.AppendLine("<td align='center'style='color:blue' valign='middle'>" +
                                        dgvc.Value.ToString() + "</td>");
                        }
                        else
                        {
                            strB.AppendLine("<td align='center'style='color:blue' valign='middle'>" +
                                        "" + "</td>");
                        }

                    }
                    strB.AppendLine("</tr>");

                }
                //table footer & end of html file
                strB.AppendLine("</table></center></body></html>");


                SaveFileDialog sfd = new SaveFileDialog();

                sfd.Filter = "Text file(*.html)|*.html";
                sfd.FilterIndex = 1;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.Cancel) { return; }
                string dirLocationString = sfd.FileName;
                // program file save location dte hobe
                System.IO.File.WriteAllText(dirLocationString, strB.ToString());
                System.Diagnostics.Process.Start(dirLocationString);
            
        }

        private void SearchBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(SearchBox.Text))
                {
                    (dataGridView1.DataSource as DataTable).DefaultView.RowFilter = string.Format("Message LIKE '%{0}%' ", SearchBox.Text);
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        //link creation
                        DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                        updatecell.Value = loadedComments[i].postlink;
                        dataGridView1.Rows[i].Cells[4] = updatecell;
                        DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                        inboxcell.Value = loadedComments[i].inbox;
                        dataGridView1.Rows[i].Cells[10] = inboxcell;

                        DataGridViewCheckBoxCell islike = new DataGridViewCheckBoxCell();
                        if (loadedComments[i].islike == "True")
                            islike.Value = true;
                        else
                        {
                            islike.Value = false;
                        }

                        dataGridView1.Rows[i].Cells[7] = islike;

                        DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                        if (loadedComments[i].ishidden == "True")
                            ishidden.Value = true;
                        else
                        {
                            ishidden.Value = false;
                        }

                        dataGridView1.Rows[i].Cells[8] = ishidden;

                        DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                        if (loadedComments[i].canreply == "True")
                            canreply.Value = canreply.FalseValue;
                        else
                        {
                            canreply.Value = canreply.TrueValue;
                        }

                        dataGridView1.Rows[i].Cells[9] = canreply;

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
                        updatecell.Value = loadedComments[i].postlink;
                        dataGridView1.Rows[i].Cells[4] = updatecell;
                        DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                        inboxcell.Value = loadedComments[i].inbox;
                        dataGridView1.Rows[i].Cells[10] = inboxcell;

                        DataGridViewCheckBoxCell islike = new DataGridViewCheckBoxCell();
                        if (loadedComments[i].islike == "True")
                            islike.Value = true;
                        else
                        {
                            islike.Value = false;
                        }

                        dataGridView1.Rows[i].Cells[7] = islike;

                        DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                        if (loadedComments[i].ishidden == "True")
                            ishidden.Value = true;
                        else
                        {
                            ishidden.Value = false;
                        }

                        dataGridView1.Rows[i].Cells[8] = ishidden;

                        DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                        if (loadedComments[i].canreply == "True")
                            canreply.Value = canreply.FalseValue;
                        else
                        {
                            canreply.Value = canreply.TrueValue;
                        }

                        dataGridView1.Rows[i].Cells[9] = canreply;

                        progressBar1.Hide();
                    }
                }

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


                if (CurrentPageId != "0" && Profile_Access_token != null)
                {
                    if (e.ColumnIndex == 4)
                    {
                        dynamic CurrentPageTokenCall = fb.Get(CurrentPageId + "?fields=access_token");

                        //on click redirect to browser inbox

                        try
                        {
                            string msg = dataGridView1.CurrentRow.Cells[13].Value.ToString();
                            System.Diagnostics.Process.Start(msg);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                    }
                    if(e.ColumnIndex == 10)
                    {
                        dynamic CurrentPageTokenCall = fb.Get(CurrentPageId + "?fields=access_token");

                        //on click redirect to browser inbox

                        try
                        {
                            string msg = dataGridView1.CurrentRow.Cells[10].Value.ToString();
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
            for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
            {
                //link creation
                DataGridViewLinkCell updatecell = new DataGridViewLinkCell();
                updatecell.Value = loadedComments[i].postlink;
                dataGridView1.Rows[i].Cells[4] = updatecell;
                DataGridViewLinkCell inboxcell = new DataGridViewLinkCell();
                inboxcell.Value = loadedComments[i].inbox;
                dataGridView1.Rows[i].Cells[10] = inboxcell;

                DataGridViewCheckBoxCell islike = new DataGridViewCheckBoxCell();
                if (loadedComments[i].islike == "True")
                    islike.Value = true;
                else
                {
                    islike.Value = false;
                }

                dataGridView1.Rows[i].Cells[7] = islike;

                DataGridViewCheckBoxCell ishidden = new DataGridViewCheckBoxCell();
                if (loadedComments[i].ishidden == "True")
                    ishidden.Value = true;
                else
                {
                    ishidden.Value = false;
                }

                dataGridView1.Rows[i].Cells[8] = ishidden;

                DataGridViewCheckBoxCell canreply = new DataGridViewCheckBoxCell();
                if (loadedComments[i].canreply == "True")
                    canreply.Value = canreply.FalseValue;
                else
                {
                    canreply.Value = canreply.TrueValue;
                }

                dataGridView1.Rows[i].Cells[9] = canreply;

                progressBar1.Hide();
            }

        }
    }
}
