using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WaveObserver : MonoBehaviour
{
    private List<IWaveObserver> _waveObservers = new List<IWaveObserver>();

    public void addObserver(IWaveObserver waveObserver)
    {
        _waveObservers.Add(waveObserver);
    }
    public void removeObserver(IWaveObserver waveObserver)
    {
        _waveObservers.Remove(waveObserver);
    }

    protected void notifyChangeWave()
    {
        _waveObservers.ForEach((_waveOberver) =>
        {
            _waveOberver.ChangeWave(); 
        }
        ) ;
    }

    protected void notifyChangeEnemyCount()
    {
        _waveObservers.ForEach((_waveObserver) => 
        {
            _waveObserver.ChangeEnemyCount();
        }
        );
    }

    protected void notifyChangeScore(string EnemyType)
    {
        _waveObservers.ForEach((_waveObserver) =>
        {
            _waveObserver.ChangeScore(EnemyType);
        }
        );
    }
    
}


public interface IWaveObserver
{
    public void ChangeWave();
    public void ChangeEnemyCount();
    public void ChangeScore(string EnemyType);
}