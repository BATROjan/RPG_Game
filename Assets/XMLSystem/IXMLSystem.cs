using System.Collections.Generic;
using Bullet;
using Enemy;
using Gun;

namespace XMLSystem
{
    public interface IXMLSystem
    {
        void CreatXMLFile();
        void SaveHealthToXML(string health);
        void SaveEnemyCountToXML(List<EnemyView> list);
        void SaveGunInBackPackToXML(Dictionary<GunView, float> dictionary);
        void SaveBulletToXML(Dictionary<BulletView, float> dictionary);
        string LoadFromXML(string key);
    }
}