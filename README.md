# ARPG-Demo

基于 Unity 的动作角色扮演游戏战斗练习项目，实现了技能系统、UI 框架、有限状态机、对象池等核心模块。

演示视频链接： [【28届实习，ARPG项目演示】](https://www.bilibili.com/video/BV1k4WB6rED8?vd_source=f5e89f8dc4896c7a1d8d179eab8ae72e)

## 游戏流程

### 1. 开始界面
提供「开始新游戏」与「继续游戏」入口。使用本地 Sqlite 表实现简单的数据存档
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

### 5. 关卡选择
<img width="724" height="424" alt="屏幕截图 2026-11-13 193759" src="https://github.com/user-attachments/assets/b3ea31db-eeff-46e7-942e-83b96f4b56c7" />

### 6. 角色技能与敌人状态机
<img width="723" height="425" alt="屏幕截图 2026-11-13 193852" src="https://github.com/user-attachments/assets/ac61bdfc-486b-44ae-8ebf-266d9ff9d5da" />
<img width="727" height="419" alt="屏幕截图 2026-11-13 193815" src="https://github.com/user-attachments/assets/f3667bcd-86d8-4572-83d3-b6a85a7ff9ba" />

## 核心技术

- **有限状态机（FSM）**：统一管理敌人待机、移动、攻击、追击、死亡等状态切换。
- **技能系统**：配置驱动的技能释放逻辑，支持群体伤害、持续伤害、冷却管理等效果，支持多种技能判定方式
- **UI 框架**：实现简单 UI 框架，使用窗口管理交互元素，减少耦合。
- **对象池**：统一管理子弹、技能特效等高频创建对象，减少运行时 GC。
- **设计模式实践**：单例、工厂、对象池等在战斗系统中的实际落地。

## 技术栈

| 项目 | 说明 |
|---|---|
| 引擎 | Unity 2022.3.55f1c1 |
| 语言 | C# |
| UI | UGUI + TextMeshPro |
| 数据 | SQLite（`Assets/StreamingAssets/ARPG.db`）+ ScriptableObject 技能配置 |
| 相机 | 自写第三人称相机（右键转视角、滚轮缩放） |
| 动画 | 通用 AnimationEvent 桥 + 技能命中帧触发 |

## 功能模块

| 模块 | 说明 | 位置 |
|---|---|---|
| 技能系统 | 裂地斩 / 旋风斩 / 烈焰风暴 / 霜华新星，冷却、消耗、范围判定、对象池 | `Assets/MyScript/MyHelper/SkillFrameWork/` |
| 技能数据 | 技能从数据库迁移到 ScriptableObject，Inspector 直接配数值 | `Assets/Resources/SkillConfig/Job*_SkillSet.asset` |
| 技能特效 | 4 个表现 prefab，按时间轴依次播放起手 / 命中 / 余留，带点光闪光 | `Assets/MyPerfabs/Skill/` |
| 敌人 AI | 有限状态机管理待机 / 巡逻 / 追击 / 攻击 / 死亡 | `Assets/MyScript/MyHelper/` |
| 第三人称相机 | 相机与角色根节点平级，角色转身不影响视角；WASD 相对相机方向移动 | `ThirdPersonCameraController.cs`、`CharacterMotor.cs` |

## 目录结构

```
Assets/
├─ MyScript/              游戏代码（核心逻辑、UI、技能、数据）
├─ Scenes/                场景：StartScene / SelectHeroScene / UIMainCity / BattleScene / GameMain / LoadindScene
├─ MyPerfabs/             预制体（角色、技能、场景物件）
├─ Resources/             运行时 Resources.Load 资源（角色、技能、技能配置）
├─ MyRes/                 美术资源：模型、材质、贴图、字体、音效、程序化生成环境/UI
├─ StreamingAssets/       数据库与配置表
├─ PlayerPart/            输入系统与角色 Animator Controller
├─ Plugins/               第三方插件（Joystick Pack 等）
└─ TextMesh Pro/          TMP 基础资源
Packages/                 Unity 包依赖
ProjectSettings/          工程设置
```


## 说明

- 技能数据在 `Assets/Resources/SkillConfig/`，改数值直接在 Inspector 里改，无需动代码。
- 场景中的程序化环境素材在 `Assets/MyRes/GeneratedEnv/`（模型 / 预制体 / 材质 / Shader）。
- 角色动画事件统一为 `SkillTrigger`，挂在 Animator 同物体的 `AnimationEvent` 组件上。


