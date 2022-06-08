using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LoadEvents : MonoBehaviour
{
    public List<EventSystem> EventArrList = new List<EventSystem>();
    public List<EventSystem> EventArrList50 = new List<EventSystem>();
    public List<EventSystem> EventArrList25 = new List<EventSystem>();
    public List<EventSystem> EventArrList5 = new List<EventSystem>();
    
    private EventSystem[] EventArr;
    private EventSystem[] EventArr50;
    private EventSystem[] EventArr25;
    private EventSystem[] EventArr5;

    void Start()
    {
        print("Tum SO lar yukleniyor");
        
        
        // Bunun gibi olacak, array aç
        EventArr = Resources.LoadAll<EventSystem>("Event/Normal");
        EventArrList = EventArr.ToList();
        
        EventArr50 = Resources.LoadAll<EventSystem>("Event/%50moral");
        EventArrList50 = EventArr50.ToList();

        EventArr25 = Resources.LoadAll<EventSystem>("Event/%25moral");
        EventArrList25 = EventArr25.ToList();

        EventArr5 = Resources.LoadAll<EventSystem>("Event/%5moral");
        EventArrList5 = EventArr5.ToList();
    }
}
