using Application.Interface;
using AutoMapper;
using Entities.Entity;
using Entities.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
using System.Collections.Generic;
using WebAPI.DTO;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class NewsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly INewsApplication _newsApplication;

        public NewsController(IMapper mapper,
            INewsApplication newsApplication)
        {
            _mapper = mapper;
            _newsApplication = newsApplication;
        }

        [Produces("application/json")]
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<NewsEntity> listEntity = await _newsApplication.Get();
                List<NewsDTO> listDto = _mapper.Map<List<NewsDTO>>(listEntity);
                return Ok(listDto);
            } catch (Exception)
            {
                return Problem(string.Empty, "Sorry, we had an internal problem");
            }
        }

        [Produces("application/json")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                NewsEntity? news = await _newsApplication.Get(id);
                if (news == null)
                {
                    return NotFound($"Not found News {id}");
                }

                var dto = _mapper.Map<NewsDTO>(news);
                return Ok(dto);
            } catch (Exception)
            {
                return Problem(string.Empty, "Sorry, we had an internal problem");
            }
        }

        [Produces("application/json")]
        [HttpPost()]
        public async Task<IActionResult> Post(NewsDTO dto)
        {
            try
            {
                var news = new NewsEntity();
                news.Title = dto.Title;
                news.Information = dto.Information;
                news.UserId = GetIdUserLogged();

                NewsEntity? entity = await _newsApplication.Add(news);
                if (entity == null)
                {
                    if (news.Notifications.Any())
                    {
                        string errors = string.Join(", ", news.Notifications.Select(x =>
                                            $"{x.NameProperties} - {x.Message}"
                                        ));
                        return Problem(errors);
                    }

                    return Problem("Sorry, we had an internal problem in save news");
                }

                List<NewsEntity> listEntity = await _newsApplication.Get();
                List<NewsDTO> listDto = _mapper.Map<List<NewsDTO>>(listEntity); 
                return Ok(listDto);
            } catch (Exception)
            {
                return Problem(string.Empty, "Sorry, we had an internal problem");
            }
        }

        [Produces("application/json")]
        [HttpPut()]
        public async Task<IActionResult> Put(NewsDTO dto)
        {
            try
            {
                NewsEntity? news = await _newsApplication.Get(dto.Id);
                if (news == null)
                {
                    return NotFound();
                }

                news.Title = dto.Title;
                news.Information = dto.Information;
                news.Status = dto.Status;
                news.UserId = GetIdUserLogged();

                var entity = await _newsApplication.Update(news);
                if (entity == null)
                {
                    if (news.Notifications.Any())
                    {
                        string errors = string.Join(", ", news.Notifications.Select(x =>
                                            $"{x.NameProperties} - {x.Message}"
                                        ));
                        return Problem(errors);
                    }
                    return Problem("Sorry, we had an internal problem in update news");
                }

                List<NewsEntity> listEntity = await _newsApplication.Get();
                List<NewsDTO> listDto = _mapper.Map<List<NewsDTO>>(listEntity);
                return Ok(listDto);
            } catch (Exception)
            {
                return Problem(string.Empty, "Sorry, we had an internal problem");
            }
        }

        [Produces("application/json")]
        [HttpDelete()]
        public async Task<IActionResult> Delete(NewsDTO dto)
        {
            try
            {
                NewsEntity? news = await _newsApplication.Get(dto.Id);
                if (news == null)
                {
                    return NotFound();
                }

                var entity = await _newsApplication.Delete(news);
                if (entity == null)
                {
                    return Problem("Sorry, we had an internal problem in delete news");
                }

                List<NewsEntity> listEntity = await _newsApplication.Get();
                List<NewsDTO> listDto = _mapper.Map<List<NewsDTO>>(listEntity);
                return Ok(listDto);
            } catch (Exception)
            {
                return Problem(string.Empty, "Sorry, we had an internal problem");
            }
        }

        private string GetIdUserLogged()
        {
            // User do Claims
            if (User != null)
            {
                return User.FindFirst(Const.UserIdLoggedAPI)?.Value ?? string.Empty;
            }

            return string.Empty;
        }
    }
}
