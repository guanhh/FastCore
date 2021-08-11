# FastCore

#### 介绍

**快速开发后端接口，将关注的聚焦于业务实现**


**轻量简单、结构分明、开箱即用**


基于 AspNetCore 的后端接口快速开发框架，包含基本的认证授权、审计日志、EFCore、Swagger、Redis、Hangfire 等功能模块。

#### 软件架构
软件架构说明


#### 安装教程

安装前准备：

1. Visual Studio 2019
2. .NET5
3. SQL Server 2014 及以上版本
4. Redis

运行前准备：

1. 配置 `appsetting.json` 中数据库连接字符串
2. `Hangfire` 需要先创建数据表，在 SQL Server 管理工具中执行 `CREATE DATABASE FastCore_Hangfire`
3. 将 `extension` 目录下 `Application` 结尾的程序集编译，并将 `dll` 复制到 `FastCore` 可执行程序目录下

默认地址：
- Swagger 地址：http://localhost:5000/swagger
- HealthCheck 地址：http://localhost:5000/healthchecks-ui
- Hangfire 地址：http://localhost:5000/hangfire

截图：

![swagger](https://gitee.com/guan2h/fast-core/raw/master/doc/images/open-api.PNG)

![healthchecks](https://gitee.com/guan2h/fast-core/raw/master/doc/images/healthchecks-ui.PNG)

![hangfire_ui](https://gitee.com/guan2h/fast-core/raw/master/doc/images/hangfire_ui.PNG)

![hangfire_task](https://gitee.com/guan2h/fast-core/raw/master/doc/images/hangfire_task.PNG)

![get-token](https://gitee.com/guan2h/fast-core/raw/master/doc/images/get-token.PNG)

![refresh-token](https://gitee.com/guan2h/fast-core/raw/master/doc/images/refresh-token.PNG)

#### 使用说明

1.  xxxx
2.  xxxx
3.  xxxx

#### 参与贡献

1.  Fork 本仓库
2.  新建 Feat_xxx 分支
3.  提交代码
4.  新建 Pull Request


#### 特技

1.  使用 Readme\_XXX.md 来支持不同的语言，例如 Readme\_en.md, Readme\_zh.md
2.  Gitee 官方博客 [blog.gitee.com](https://blog.gitee.com)
3.  你可以 [https://gitee.com/explore](https://gitee.com/explore) 这个地址来了解 Gitee 上的优秀开源项目
4.  [GVP](https://gitee.com/gvp) 全称是 Gitee 最有价值开源项目，是综合评定出的优秀开源项目
5.  Gitee 官方提供的使用手册 [https://gitee.com/help](https://gitee.com/help)
6.  Gitee 封面人物是一档用来展示 Gitee 会员风采的栏目 [https://gitee.com/gitee-stars/](https://gitee.com/gitee-stars/)
