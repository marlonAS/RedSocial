using PostsServices.DB;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace PostsServices.Controllers
{
    public class LikesController : ApiController
    {
        private RedSocialEntities db = new RedSocialEntities();

        // GET: api/Likes
        public IQueryable<Likes> GetLikes()
        {
            return db.Likes;
        }

        // GET: api/Likes/5
        [ResponseType(typeof(Likes))]
        public IHttpActionResult GetLikes(int id)
        {
            Likes likes = db.Likes.Find(id);
            if (likes == null)
            {
                return NotFound();
            }

            return Ok(likes);
        }

        // PUT: api/Likes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLikes(int id, Likes likes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != likes.Id)
            {
                return BadRequest();
            }

            db.Entry(likes).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LikesExists(id))
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

        // POST: api/Likes
        [ResponseType(typeof(Likes))]
        public IHttpActionResult PostLikes(Likes likes)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Likes olikes = db.Likes.Where(p => p.PostId == likes.PostId && p.UserID == likes.UserID).FirstOrDefault();
            if (olikes == null)
            {
                db.Likes.Add(likes);
            }
            else
                db.Likes.Remove(olikes);

            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = likes.Id }, likes);
        }

        // DELETE: api/Likes/5
        [ResponseType(typeof(Likes))]
        public IHttpActionResult DeleteLikes(int id)
        {
            Likes likes = db.Likes.Find(id);
            if (likes == null)
            {
                return NotFound();
            }

            db.Likes.Remove(likes);
            db.SaveChanges();

            return Ok(likes);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LikesExists(int id)
        {
            return db.Likes.Count(e => e.Id == id) > 0;
        }
    }
}