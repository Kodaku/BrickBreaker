using UnityEngine;
using System.IO;
public class Observation
{
    float m_episodeDuration;
    int m_numberOfSteps;
    float m_qMin;
    float m_qMax;
    float m_qValue;
    int m_brokenBlocks;
    int m_hitCount;

    public float episodeDuration
    {
        get { return m_episodeDuration; }
        set { m_episodeDuration = value; }
    }
    public int numberOfSteps
    {
        get { return m_numberOfSteps; }
        set { m_numberOfSteps = value; }
    }
    public float qMin
    {
        get { return m_qMin; }
        set { m_qMin = value; }
    }
    public float qMax
    {
        get { return m_qMax; }
        set { m_qMax = value; }
    }
    public float qValue
    {
        get { return m_qValue; }
        set { m_qValue = value; }
    }
    public int brokenBlocks
    {
        get { return m_brokenBlocks; }
        set { m_brokenBlocks = value; }
    }
    public int hitCount
    {
        get { return m_hitCount; }
        set { m_hitCount = value; }
    }
    public void SaveToFile(bool append = true)
    {
        string tsvPath = Application.dataPath + "/Resources/Observations.tsv";
        string tsvData = m_episodeDuration.ToString().Replace(",", ".") + "\t" +
                        m_numberOfSteps + "\t" +
                        m_qMin.ToString().Replace(",", ".") + "\t" +
                        m_qMax.ToString().Replace(",", ".") + "\t" +
                        qValue.ToString().Replace(",",".") + "\t" +
                        m_brokenBlocks + "\t" +
                        m_hitCount;
        StreamWriter tsvWriter = new StreamWriter(tsvPath, append);
        tsvWriter.WriteLine(tsvData);
        tsvWriter.Flush();
        tsvWriter.Close();
    }
}