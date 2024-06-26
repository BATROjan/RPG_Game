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
        void SaveGunInBackPackToXML(List<GunView> list);
        void SaveBulletToXML(CellView cellView);
        string LoadFromXML(string key, string value);
    }
}