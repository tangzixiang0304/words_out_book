using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace words_out_book
{

    public partial class Form1 : Form
    {
        public WordDictionary<string, int> Ana(string text)
        {
            var words = new List<string>();
            var dict = new WordDictionary<string, int>();
            byte[] bts = Encoding.Unicode.GetBytes(text);
            //foreach (var bt in bts)
            //{
            //    Console.Write(string.Format("{0} ",bt));
            //}
            //Console.WriteLine();
            var pointer = 0;
            var i = 0;
            while (i <= bts.Length - 2)
            {
                byte[] tmp;

                if (bts[i + 1] == 0 && bts[i] != 32)
                {
                    pointer = i;
                    while (pointer + 2 < bts.Length && bts[pointer + 2] != 32 && bts[pointer + 2 + 1] == 0)
                    {
                        pointer += 2;
                    }
                    var len = pointer + 2 - i;
                    tmp = new byte[len];
                    Array.Copy(bts, i, tmp, 0, len);
                    i = pointer + 2;
                }
                else if (bts[i] == 32 && bts[i + 1] == 0)
                {
                    i += 2;
                    continue;
                }
                else
                {
                    tmp = new byte[] { bts[i], bts[i + 1] };
                    i += 2;
                }
                var word = Bytes2Word(tmp);
                words.Add(word);
                Put(dict, Bytes2Word(tmp));
            }
            return dict;
        }


        string Bytes2Word(byte[] bytes)
        {
            return Encoding.Unicode.GetString(bytes);
        }

        void Put(Dictionary<string, int> dict, string word)
        {
            int value;
            if (dict.TryGetValue(word, out value))
            {
                dict[word] = value + 1;
            }
            else
            {
                dict[word] = 1;
            }

        }


        public string filePath;
        public string fileText;
        public Dictionary<char,int> wordFrequency;
        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void listBox1_DragDrop(object sender, DragEventArgs e)
        {
            listBox1.Items.Clear();
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop);
            listBox1.Items.AddRange(s);
            filePath = s[0];
            MessageBox.Show(filePath);
        }



        private void open_file_btn_Click(object sender, EventArgs e)
        {
                //文件路径
            try
            {
                if (File.Exists(filePath))
                {
                    fileText = File.ReadAllText(filePath);
                    WordDictionary<string, int> retDict = Ana(fileText);
                    retDict.OrderBy(o => o.Value);
                    foreach(string x in retDict.Keys)
                    {
                        listBox2.Items.Add("word:"+x+" times:"+retDict[x]);
                    }
                    MessageBox.Show(retDict.ToString());
                    //foreach (char word in fileText)
                    //{
                    //    if (wordFrequency.ContainsKey(word))
                    //    {
                    //        wordFrequency[word]++;
                    //    }

                    //    else
                    //    {
                    //        wordFrequency.Add(word, 1);
                    //    }


                    //}
                    //MessageBox.Show(wordFrequency.ToString());

                    //byte[] mybyte = Encoding.UTF8.GetBytes(text1.Text);
                    //text1.Text = Encoding.UTF8.GetString(mybyte);
                }
                else
                {
                    MessageBox.Show("文件不存在");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
