using UnityEngine;

namespace _src.Managers
{
    public class ShopManager : MonoBehaviour
    {
        [SerializeField] private Canvas cityCanvas;
        [SerializeField] private AudioManager audioManager;

        public void OnDayEnd(in int dayCount)
        {
            if(ShouldShopOpen(dayCount))
                OpenShop();
            
            else
                CloseShop();
        }
        
        private bool ShouldShopOpen(in int dayCount)
        {
            return dayCount == 10;
        }

        private void CloseShop()
        {
            cityCanvas.gameObject.SetActive(false);
            audioManager.ChangeAudioClip(MusicTracks.MainMusic);
        }

        private void OpenShop()
        {
            cityCanvas.gameObject.SetActive(true);
            audioManager.ChangeAudioClip(MusicTracks.ShopMusic);
        }
    }
}