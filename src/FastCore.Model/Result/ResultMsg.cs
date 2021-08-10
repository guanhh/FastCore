using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastCore.Model.Result
{
    public class ResultMsg<T>
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T data { get; set; }

        /// <summary>
        /// 反馈信息
        /// </summary>
        public string message { get; set; }

    }

    public class Data<T>
    {
        public int total { get; set; }

        public T items { get; set; }
    }
}
