# ARPG-Demo

基于 Unity 的动作角色扮演游戏战斗练习项目，实现了技能系统、UI 框架、有限状态机、对象池等核心模块。

## 游戏流程


### 1. 开始界面
提供「开始新游戏」与「继续游戏」入口。使用本地Sqlite表实现简单的数据存档
<img width="725" height="421" alt="屏幕截图 2026-11-13 193655" src="https://github.com/user-attachments/assets/14275783-12d9-407c-bb56-0ce30326207f" />



### 2. 角色选择
选择不同角色按钮，展示角色，显示角色信息

**战士** —— 近战输出职业，武器为砍刀，拥有强大的力量与体力，可正面摧毁敌人防御，推荐新手使用。
<img width="722" height="422" alt="屏幕截图 2026-11-13 193720" src="https://github.com/user-attachments/assets/9a5026d6-e110-44e1-966b-984e1efc1e7e" />


**法师** —— 远程输出职业，武器为法杖，自身体力较弱，依靠法术进行远距离范围攻击。
<img width="724" height="421" alt="屏幕截图 2026-11-13 193724" src="https://github.com/user-attachments/assets/4ec5ec44-31ae-4cde-bd87-4a1c070c1f5c" />


### 3. 主城场景
<img width="721" height="422" alt="屏幕截图 2026-11-13 193744" src="https://github.com/user-attachments/assets/c1cdbc28-ba69-4bdd-82b5-b0a1493ab2f9" />


### 4. 技能选择面板
为角色选择技能，到战斗场景中会读取配置的数据，灵活配置
<img width="728" height="423" alt="屏幕截图 2026-11-13 193751" src="https://github.com/user-attachments/assets/fdb0f7ff-ce79-4492-9c36-9b9e6ed41ce3" />


### 4. 关卡选择
<img width="724" height="424" alt="屏幕截图 2026-11-13 193759" src="https://github.com/user-attachments/assets/b3ea31db-eeff-46e7-942e-83b96f4b56c7" />


### 5. 角色技能与敌人状态机
<img width="723" height="425" alt="屏幕截图 2026-11-13 193852" src="https://github.com/user-attachments/assets/ac61bdfc-486b-44ae-8ebf-266d9ff9d5da" />
<img width="727" height="419" alt="屏幕截图 2026-11-13 193815" src="https://github.com/user-attachments/assets/f3667bcd-86d8-4572-83d3-b6a85a7ff9ba" />



## 核心技术

- **有限状态机（FSM）**：统一管理敌人待机、移动、攻击、追击、死亡等状态切换。
- **技能系统**：配置驱动的技能释放逻辑，支持群体伤害、持续伤害、冷却管理等效果，支持多种技能判定方式
- **UI 框架**：实现简单UI框架，使用窗口管理交互元素，减少耦合。
- **对象池**：统一管理子弹、技能特效等高频创建对象，减少运行时 GC。
- **设计模式实践**：单例、工厂、对象池等在战斗系统中的实际落地。
