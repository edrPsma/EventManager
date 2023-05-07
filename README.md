# EventManager
 ### 入门
#### 简易事件
对于简易事件，即一个参数事件的注册可使用
```Register<TEvent>(object eventName, Action<TEvent> onEvent);```
这个带有事件名的接口，TEvent为int、bool等基础数据类型。
同时，为了响应该事件，可使用
```Trigger<TEvent>(object eventName, TEvent e);```
下面是一个简单的例子：
Player类模拟玩家，该玩家类定义了一个玩家事件的枚举、玩家血量的成员变量以及一个用于模拟玩家扣血的按钮。HpPanel类则是用于显示玩家当前血量的UI面板类，该类监听了玩家Hp事件，Change方法为事件触发后的回调方法。当我们点击按钮，玩家的血量会减少并且同时触发玩家Hp事件，由于HpPanel类中监听了玩家Hp事件，因此当玩家血量减少时，面板会同步更新玩家的血量信息。

``` Unity3D
public class Player : MonoBehaviour
{
	public enum PlayerEvent { Hp }
    public int hp=10;
    public Button button;
    
    private void Awake()
    {
	    button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            hp--;
            EventManager.Instance.Trigger<int>(PlayerEvent.Hp,hp);
        });
    }
}
```

``` Unity3D
public class HpPanel : MonoBehaviour
{
    Text text;
    
    private void Awake()
    {
        text = GetComponent<Text>();
        EventManager.Instance.Register<int>(Player.PlayerEvent.Hp, Change).Bind(this);
    }
    
    void Change(int value) { text.text = value.ToString(); }
}
```


对于上述例子中，每当玩家Hp减少，我们都要手动触发一次事件（即调用一次Trigger方法），不然面板是不会进行同步的。这显得很麻烦，有没有更简单的方法？我们很容易就能想到只要把Hp变成属性，这样只要在Set方法中调用Trigger方法就可以了。但还不够简便，每次都写一个Get/Set还是比较麻烦，我提供了一个BindableProperty类来解决这个问题。下面来重构一下Player类代码
``` Unity3D
public class Player : MonoBehaviour
{
	public enum PlayerEvent { Hp }
    public BindableProperty<int> hp = new BindableProperty<int>(PlayerEvent.Hp, 999);
    public Button button;
    
    private void Awake()
    {
	    button = GetComponent<Button>();
        button.onClick.AddListener(() =>
        {
            hp.Value--;
        });
    }
}
```

#### 复杂事件
对于复杂事件，即多个参数或者逻辑复杂的事件的注册可使用
```IUnRegister Register<TEvent>(Action<TEvent> onEvent) where TEvent : new();```
该事件可使用结构体和类定义。注册时返回一个注销接口用于事件的注销。
响应复杂事件可使用
```void Trigger<TEvent>() where TEvent : new();```或```void Trigger<TEvent>(TEvent e);```
前者无需提供事件实例(会自动创建一个)用于无参数复杂事件。后者需要提供事件实例，用于多个参数的复杂事件。为什么复杂事件需要单独一个类或结构体来定义？因为这样可以复用，并且可以和其他类独立出来，减少其他类的负担和耦合。下面用一个血条渐变的简单例子来演示一下复杂事件的使用方法
``` Unity3D
public struct EventHpGradient
{
	public EventHpGradient(int hp)
	{
		this.hp=hp;
	}
	
	public int hp;
	
	public Excute(Slider hpBar)
	{
		//血条渐变效果
	}
    
}
```

``` Unity3D
public class Player : MonoBehaviour
{
    public int Hp
    {
	    get => Hp;
	    Set
	    { 
		    Hp=value;
		    EventManager.Instance.Trigger<EventHpGradient>(new EventHpGradient(Hp));
	    }
    }
}
```

``` Unity3D
public class HpPanel : MonoBehaviour
{
    Slider hpBar;
    
    private void Awake()
    {
        hpBar = GetComponent<Slider>();
        EventManager.Instance.Register<EventHpGradient>(e => 
        e.Excute(hpBar) ).Bind(this);
    }
}
```
血条渐变方法本来应该写在HpPanel类中，此处使用事件将其独立出来，减少了HpPanel的代码行数，方便了代码的阅读，使其能更专注于其他业务的编写。

#### Bind方法
每当我们注册一个事件后，都要在合适的地方进行事件的注销操作。注销操作一般置于OnDestory方法中。但是每次注销事件都要在OnDestory方法中写一个注销事件的方法岂不是很麻烦？为此我们提供了一个Bind扩展方法，使事件在注册时可以对一个物体进行绑定，绑定后当该事件绑定的物体销毁后事件也会进行注销操作。
