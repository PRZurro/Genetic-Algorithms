using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Cromosome 
{
    //public Cromosome()
    //{

    //}

    //public Cromosome(Cromosome parent1, Cromosome parent2, float mutationProbability)
    //    :
    //    this(parent1.Recombine(parent2, mutationProbability))
    //{}

    //public Cromosome(Cromosome other)
    //{
    //    fGenes = other.fGenes;
    //    bGenes = other.bGenes;
    //    iGenes = other.iGenes;
    //}

    Dictionary< byte, FGen >  fGenes;
    Dictionary< byte, IGen >  iGenes;
    Dictionary< byte, BGen >  bGenes;


    //struct GenWheel
    //{
    //    public float force;
    //    public float dampingRatio;
    //    public float frequency;
    //    public float scale; // Size
    //    public bool isMotor; // dudoso
    //}
    //struct GenChasis
    //{
    //    public float mass;
    //    public float scale; // Size
    //    public float type;
    //}

    //struct GenSwingArm
    //{
    //    public float maxAngle;
    //    public float scaleX;
    //}

    //////////////////////////////////////////RECOMBINATION////////////////////////////////
    //Cromosome Recombine(Cromosome other, float mutationFactor)
    //{
    //    return new Cromosome
    //    (
    //    );
    //}

    //bool ObtainGen(bool gen1, bool gen2, float probabilityGen1, float mutationFactor)
    //{
    //    if (Random.Range(0.0f, 100f) < mutationFactor)
    //    {
    //        return Mutate();
    //    }
    //    else
    //    {
    //        return ChooseGen(gen1, gen2, probabilityGen1);
    //    }
    //}

    //float ObtainGen(float gen1, float gen2, float probabilityGen1, float mutationFactor, float minGenValue, float maxGenValue)
    //{
    //    if (Random.Range(0.0f, 100f) < mutationFactor)
    //    {
    //        return Mutate(minGenValue, maxGenValue);
    //    }
    //    else
    //    {
    //        return ChooseGen(gen1, gen2, probabilityGen1);
    //    }
    //}

    //bool ChooseGen(bool gen1, bool gen2, float probabilityGen1)
    //{
    //    if (Random.Range(0.0f, 100.0f) < probabilityGen1)
    //    {
    //        return gen1;
    //    }
    //    else
    //    {
    //        return gen2;
    //    }
    //}

    //float ChooseGen(float gen1, float gen2, float probabilityGen1)
    //{
    //    if (Random.Range(0.0f, 100.0f) < probabilityGen1)
    //    {
    //        return gen1;
    //    }
    //    else
    //    {
    //        return gen2;
    //    }
    //}
    //void Mutate(Gen<bool> gen)
    //{
    //    return (Random.value > 0.5f);
    //}
}