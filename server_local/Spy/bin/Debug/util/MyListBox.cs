using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

/*
 * 自定义ListBox控件
 * 作用于 Selection窗体
 */ 
namespace Spy.util
{
    public partial class MyListBox : ListBox
    {
        MyListBoxItem UnderMouseItem = null;
        public MyListBox()
        {
            this.DrawMode = DrawMode.OwnerDrawVariable;
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true); //！！！
            UpdateStyles();
        }
        //设置各项高度
        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            try
            {
                if (e.Index >= 0)
                {
                    base.OnMeasureItem(e);
                    MyListBoxItem i = Items[e.Index] as MyListBoxItem;
                    if (i != null)
                    {
                        e.ItemHeight = 60;
                    }
                }
            }
            catch
            {

            }
        }
        //重绘
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            for (int i = 0; i < Items.Count; i++)
            {
                MyListBoxItem item = Items[i] as MyListBoxItem;
                if (item != null)
                {
                    Rectangle bound = GetItemRectangle(i);
                    if (i == this.SelectedIndex)//选中项
                    {
                        e.Graphics.FillRectangle(Brushes.Gray, bound);
                        e.Graphics.DrawImage(item.img, new Rectangle(bound.Left + 5, bound.Top + 6, 40, 48));
                        e.Graphics.DrawString("   使用者："+item.userName + "                               所属部门：" + item.department, new Font("微软雅黑", 10), Brushes.White, new PointF(bound.Left + 5 + 50, bound.Top + 6));
                        e.Graphics.DrawString("   计算机编号：" + item.id + "                 IP：" + item.ip, new Font("微软雅黑", 11), Brushes.White, new PointF(bound.Left + 5 + 50, bound.Top + 20 + 6));
                    }
                    else if (item == UnderMouseItem) //鼠标滑过
                    {
                        e.Graphics.FillRectangle(Brushes.Ivory, bound);
                        e.Graphics.DrawImage(item.img, new Rectangle(bound.Left + 5, bound.Top + 6, 40, 48));
                        e.Graphics.DrawString("    使用者：" + item.userName + "                            所属部门：" + item.department, new Font("微软雅黑", 10), Brushes.Black, new PointF(bound.Left + 5 + 50, bound.Top + 6));
                        e.Graphics.DrawString("    计算机编号：" + item.id + "                   IP：" + item.ip, new Font("微软雅黑", 11), Brushes.Black, new PointF(bound.Left + 5 + 50, bound.Top + 20 + 6));
                    }
                    else  //正常项
                    {
                        e.Graphics.DrawImage(item.img, new Rectangle(bound.Left + 5, bound.Top + 6, 40, 48));
                        e.Graphics.DrawString("   使用者：" + item.userName + "                           所属部门：" + item.department, new Font("微软雅黑", 10), Brushes.Black, new PointF(bound.Left + 5 + 50, bound.Top + 6));
                        e.Graphics.DrawString("   计算机编号：" + item.id + "                   IP：" + item.ip, new Font("微软雅黑", 11), Brushes.Black, new PointF(bound.Left + 5 + 50, bound.Top + 20 + 6));
                    }
                }

            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            for (int i = 0; i < Items.Count; ++i)
            {
                Rectangle rect = this.GetItemRectangle(i);
                if (rect.Contains(e.Location))
                {
                    UnderMouseItem = Items[i] as MyListBoxItem;
                    Invalidate();
                    break;
                }
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }

    }
}
