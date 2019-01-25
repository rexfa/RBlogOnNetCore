using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RBlogOnNetCore.EF.Domain;
using Microsoft.Extensions.Caching.Memory;
using RBlogOnNetCore.Configuration;
using RBlogOnNetCore.Models;
using RBlogOnNetCore.EF;
using Microsoft.Extensions.Options;

namespace RBlogOnNetCore.Services
{
    public class NormalCommentService : INormalCommentService
    {
        private readonly MysqlContext _mysqlContext;
        private readonly EfRepository<NormalComment> _normalCommentRepository;
        private readonly IMemoryCache _memoryCache;
        public NormalCommentService(MysqlContext mysqlContext, IMemoryCache memoryCache)
        {
            _mysqlContext = mysqlContext;
            _normalCommentRepository = new EfRepository<NormalComment>(this._mysqlContext);
            _memoryCache = memoryCache;
        }

        public IList<NormalComment> GetNormalComments(int blogId, int size)
        {
            var comments = _memoryCache.GetOrCreate(RBMemCacheKeys.NORMALCOMMENTKEY + blogId.ToString(),entry=> {
                var query = _normalCommentRepository.Table.Where(c=> blogId != 0 ? c.BlogId == blogId : c.BlogId != -1 & c.IsDeleted==false)
                    .OrderByDescending(c => c.CreatedOn).Take(size);
                var cs = query.ToList();
                return cs;
            });
            return comments;
        }

        public NormalComment CreateNormalComment(NormalComment normalComment)
        {
            _normalCommentRepository.Insert(normalComment);
            _mysqlContext.SaveChanges();
            _memoryCache.Remove(RBMemCacheKeys.NORMALCOMMENTKEY + normalComment.BlogId.ToString());
            _memoryCache.Remove(RBMemCacheKeys.NORMALCOMMENTKEY + "0");
            return normalComment;
        }
        /// <summary>
        /// 过滤替换字符串
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public string ReplacementFilter(string original)
        {
            string[] sensitives = { "雅蔑蝶", "尾申鲸", "吉拔猫", "达菲鸡", "潜烈蟹", "吟稻雁", "法克鱿", "菊花蚕", "草泥马", "春鸽",
                "A片", "ai滋", "啊无卵", "八路", "不爽不要钱", "避孕 套", "车仑女干", "处女膜", "鄧小平", "肛jiao", "共產黨", "骸卒",
                "含鳥", "叫春", "经血", "精童", "靖国神社", "九霾", "开苞", "砍翻一条街", "砍死你", "尸虫", "十八摸", "私服", "造爱",
                "知的障害", "马勒戈壁", "沃草", "草泥马勒戈壁", "进入她身体", "挺身进入", "破鞋", "禽兽", "上床", "法 轮", "狗屁",
                "小犬蠢一狼", "梅毒", "茎候佳阴", "台聯", "民進", "親民", "國民黨", "台灣", "台独", "台湾独立", "陳水扁", "呂秀蓮",
                "宋楚瑜", "連戰", "馬英九", "謝長廷", "王俊博", "林榮一", "智凡迪", "遊戲新幹線", "新幹線", "遊戲新斷線", "新斷線",
                "智冠d", "姦", "媽的", "打砲", "雞雞", "懶叫", "官方", "做愛", "打手槍", "睪丸", "勃起", "早洩", "陽萎", "草枝麻",
                "草芝麻", "屁眼", "魔獸幣", "賣幣", "代練", "雞掰", "歪逼", "月经", "外阴", "子宫", "口交", "生殖器", "肏", "屌",
                "屄", "复制", "挂机", "洗钱", "onewg", "hlwg", "hljzwg", "pcik", "wgpj", "blog baby", "game gold", "eqsf", "to173",
                "9v 9e", "93 3cn", "viphljz", "hljzvip", "51vip", "煞笔", "乳房", "特派员", "金廷勋", "杨燕姬", "政治局", "人大",
                "萨达姆", "蒙哥马利", "华西列夫斯基", "朱可夫元帅", "史迪威", "蒙巴顿", "曼施坦因", "麦克阿瑟", "布莱德雷", "冈村宁次",
                "尼米兹", "互联", "特派员", "中条英机", "天皇", "日本", "胡锦涛", "吴邦国", "温家宝", "贾庆林", "曾庆红", "黄菊",
                "吴官正", "李长春", "罗干", "江青", "吴桂贤", "吴德", "李瑞环", "柯庆施", "胡耀邦", "李雪峰", "宋庆龄", "彭珮云",
                "王汉斌", "凯丰", "邓发", "陈伯达", "李贵鲜", "刘少奇", "华国锋", "陈独秀", "董必武", "成克杰", "瞿秋白", "蔡庆林",
                "蔡和森", "姚依林", "阿沛", "阿旺晋美", "卢福坦", "向忠发", "顾顺章", "王乐泉", "王兆国", "回良玉", "刘淇", "刘云山",
                "张立昌", "张德江", "陈良", "周永康", "俞正声", "贺国强", "郭伯雄", "曹刚川", "曾培炎", "王刚", "徐才厚", "何勇",
                "梁光烈", "廖锡龙", "共产党", "周恩来", "咨询", "包过", "保过", "详情" };
            string result = original;
            foreach (string s in sensitives)
            {
                result = result.Replace(s, "**");
            }
            return result;
        }
    }
}
