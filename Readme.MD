#### SQLite单表4亿订单，大数据测试  
`SQLite`作为嵌入式数据库的翘楚，广受欢迎！  
新生命团队自2010年以来，投入大量精力对`SQLite`进行学习研究，成功应用于各系统非致命数据场合。  

## SQLite极致性能
+ 关闭同步，设置WAL模式，Synchronous=Off;Journal Mode=WAL  
+ 插入速度 8000tps，依赖CPU，HDD/SSD差别不大，主要受限于SQLite.Data.dll的Prepare  

## SQLite大数据
+ Test项目生成4亿行订单数据，主键自增ID，订单号建立索引，文件大小26.5G  
+ Web项目，魔方+XCode，首次查询较慢，约1.6秒，需要预热  
+ 设置缓存Cache Size=2000，太大缓存会报内存不足异常  
+ 跳转到两万多页，查询1.6秒，前后页耗时毫秒级，操作系统文件映射缓存生效  
+ 查询任意订单号，耗时16毫秒以内，索引效果强大  
+ 本地内存占用250M

http://bigdata.newlifex.com/Admin
## SQLite低要求
+ 租用阿里云最低配置ECS，单核1G，1M带宽，每年300块，每天九毛钱
+ 部署BigData项目到ECS，访问正常
+ 首次查询十几秒
