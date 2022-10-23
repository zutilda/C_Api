using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace WebApplication2
{
    public class WorkoutsController : ApiController
    {
        private ZhulinaApiEntities db = new ZhulinaApiEntities();

        // GET: api/Workouts
        [ResponseType(typeof(List<Workout>))]
        public IHttpActionResult GetWorkout()
        {
            return Ok(db.Workout.ToList().ConvertAll(x=> new Workout(x)));
        }

        // GET: api/Workouts/5
        [ResponseType(typeof(Workout))]
        public IHttpActionResult GetWorkout(int id)
        {
            Workout workout = db.Workout.Find(id);
            if (workout == null)
            {
                return NotFound();
            }

            return Ok(workout);
        }

        // PUT: api/Workouts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutWorkout(int id, Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != workout.id)
            {
                return BadRequest();
            }

            db.Entry(workout).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Workouts
        [ResponseType(typeof(Workout))]
        public IHttpActionResult PostWorkout(Workout workout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Workout.Add(workout);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = workout.id }, workout);
        }

        // DELETE: api/Workouts/5
        [ResponseType(typeof(Workout))]
        public IHttpActionResult DeleteWorkout(int id)
        {
            Workout workout = db.Workout.Find(id);
            if (workout == null)
            {
                return NotFound();
            }

            db.Workout.Remove(workout);
            db.SaveChanges();

            return Ok(workout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WorkoutExists(int id)
        {
            return db.Workout.Count(e => e.id == id) > 0;
        }
    }
}