using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using PostsServices.DB;

namespace PostsServices.Controllers
{
    public class PostsController : ApiController
    {
        private RedSocialEntities db = new RedSocialEntities();

        // GET: api/Posts
        public IQueryable<Posts> GetPosts()
        {
            return db.Posts;
        }

        // GET: api/Posts/5
        [ResponseType(typeof(Posts))]
        public IHttpActionResult GetPosts(int id)
        {
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return NotFound();
            }

            return Ok(posts);
        }

        // PUT: api/Posts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPosts(int id, Posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != posts.Id)
            {
                return BadRequest();
            }

            db.Entry(posts).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostsExists(id))
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

        // POST: api/Posts
        [ResponseType(typeof(Posts))]
        public IHttpActionResult PostPosts(Posts posts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Posts.Add(posts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = posts.Id }, posts);
        }

        // DELETE: api/Posts/5
        [ResponseType(typeof(Posts))]
        public IHttpActionResult DeletePosts(int id)
        {
            Posts posts = db.Posts.Find(id);
            if (posts == null)
            {
                return NotFound();
            }

            db.Posts.Remove(posts);
            db.SaveChanges();

            return Ok(posts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PostsExists(int id)
        {
            return db.Posts.Count(e => e.Id == id) > 0;
        }
    }
}