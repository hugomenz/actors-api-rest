
using RestApiTest.Contracts;
using RestApiTest.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Mvc;

namespace RestApiTest.Repositories
{

    public class ActorRepository : IActorRepository
    {
        const string JSON_PATH = @"C:\Users\Hugo_\Documents\fullStack\__\actors-api-rest\RestApiTest\Resources\Actores.json";
        public void AddActor(Actor actor)
        {
            var actores = GetActors();
            var existeActor = actores.Exists(a => a.Id == actor.Id);
            if (existeActor)
            {
                throw new Exception("Actor with that ID already exists!");
            }
            actores.Add(actor);
            UpdateActores(actores);
        }

        public void DeleteActor(int id)
        {
            var actores = GetActors();
            var indiceActor = actores.FindIndex(a => a.Id == id);

            if (indiceActor < 0)
                throw new Exception("Actor does not exist");

            actores.RemoveAt(indiceActor);
            UpdateActores(actores);
        }

        public Actor GetActorById(int id)
        {
        try
        {
            var actores = GetActors();
            var actor = actores.FirstOrDefault(a => a.Id == id);
            return actor;
        }
        catch (Exception)
        {
            throw;
                       }
            }

        public List<Actor> GetActors()
        {
            try
            {
                var actoresFromFile = GetActorsFromFile();
                List<Actor> actores = JsonConvert.DeserializeObject<List<Actor>>(actoresFromFile);
                return actores;
            }
            catch (Exception)
            {
                throw;
            }
        }

       private void UpdateActores(List<Actor> actores)
       {
           var actoresJson = JsonConvert.SerializeObject(actores, Formatting.Indented);
           File.WriteAllText(JSON_PATH, actoresJson);
        }

        private string GetActorsFromFile()
        {
            var json = File.ReadAllText(JSON_PATH);
            return json;
        }

        public void UpdateActor(Actor actor)
        {
            var actores = GetActors();
            var indiceActor = actores.FindIndex(a => a.Id == actor.Id);

            if (indiceActor < 0)
                throw new Exception("Actor no encontrado");

            actores[indiceActor] = actor;
            UpdateActores(actores);
        }
    }
}
