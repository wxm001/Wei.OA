using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Activities;

namespace WFWindowsForms
{

    public sealed class WaitForCodeActivity : NativeActivity
    {
        // 定义一个字符串类型的活动输入参数
        public OutArgument<int> UserInputMoney { get; set; }

        public InArgument<string> BookMarkName { get; set; }

        protected override bool CanInduceIdle
        {
            get
            {
                return true;
            }
        }

        // 如果活动返回值，则从 CodeActivity<TResult>
        // 并从 Execute 方法返回该值。
        protected override void Execute(NativeActivityContext context)
        {
            string str = context.GetValue(BookMarkName);
            //创建书签，让工作流停下
            context.CreateBookmark(str,new BookmarkCallback(BookmarkCallback));
        }

        private void BookmarkCallback(NativeActivityContext context, Bookmark bookmark, object value)
        {
            //书签继续执行时首先执行
            context.SetValue(UserInputMoney,(int)value);
        }
    }
}
