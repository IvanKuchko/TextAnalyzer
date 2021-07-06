using System;
using System.Collections.Generic;
using System.Linq;
using TextAnalyzer.Server.DataAccess;
using TextAnalyzer.Server.Models;
using TextAnalyzer.Server.Models.Database;
using TextAnalyzer.Server.Services.UserService;

namespace TextAnalyzer.Server.Services.AnalyzerService
{
    public class AnalyzerService : IAnalyzerService
    {
        private readonly ApplicationDbContext _context;

        public AnalyzerService(ApplicationDbContext context) => _context = context;

        public void AddValue(User user, IEnumerable<Avatar> avatars)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(user.Id) && u.IsActive);
            if (existUser == null)
                throw new UserNotFoundException(Messages.UserNotFound);
            var avatarslist = new List<Avatar>(avatars);
            var avatarsDbo = _context.Avatars.Where(a => a.User == existUser && a.IsActive).ToList();
            var avatarsDboList = new List<Avatar>();

<<<<<<< HEAD
            avatarslist = OptimizationAvatar(avatarslist);       
=======
            OptimizationAvatar(avatarslist);       
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb

            var newAvatars = addPause(avatarsDboList, avatarslist);

            foreach (var avatar in avatarsDbo)
            {
                avatar.IsActive = false;
                _context.Avatars.Update(avatar);
            }

            foreach (var avatar in newAvatars)
            {
                var guid = Guid.NewGuid();
                _context.Avatars.Add(new AvatarDto()
                {
                    Id = guid,
                    User = existUser,
                    CharPair = avatar.CharPair,
                    Delay = avatar.Delay,
                    Identical = avatar.Identical,
                    CreationDate = DateTime.Now,
                    IsActive = true
                });
            }
            _context.SaveChanges();
        }
        private List<Avatar> OptimizationAvatar(List<Avatar> avatarslist)
        {
            int sovp = 1;
            int tObsh = 0;
            for (int i = 0; i < avatarslist.Count; i++)
            {
                if (avatarslist[i].CharPair == "Pass")
                {
                    ;
                }
                else
                {
                    tObsh = avatarslist[i].Delay;
                    for (int j = i + 1; j < avatarslist.Count; j++)
                    {
                        if (avatarslist[i].CharPair == avatarslist[j].CharPair)
                        {
                            tObsh += avatarslist[j].Delay;
                            avatarslist[j].CharPair = "Pass";
                            sovp++;
                        }
                    }
                    // Присваеваем среднее значение времени паре символов, обнуляем строки со временем и количество совпадений
                    avatarslist[i].Delay = tObsh / sovp;
                    avatarslist[i].Identical += sovp;
                    sovp = 1;
                }
            }
            return avatarslist;
        }
        
        private List<Avatar> addPause(List<Avatar> avatars, List<Avatar> temporaryAvatars)
        {
            foreach (var itemMain in avatars)
            {
                foreach (var itemTemporary in temporaryAvatars)
                {
                    if (itemMain.CharPair == itemTemporary.CharPair)
                    {
                        itemMain.Delay = (itemMain.Delay * itemMain.Identical + itemTemporary.Delay * itemTemporary.Identical) / (itemMain.Identical + itemTemporary.Identical);
                        itemMain.Identical += itemTemporary.Identical;
                        itemTemporary.CharPair = "Pass";
                        break;
                    }
                }
            }
            foreach (var item in temporaryAvatars)
            {
                if (item.CharPair != "Pass")
                {
                    avatars.Add(new Avatar()
                    {
                        CharPair = item.CharPair,
                        Delay = item.Delay,
                        Identical = item.Identical
                    });
                }
            }
            return avatars;
        }

        public string CompareWithUsers(User user, IEnumerable<User> users)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(user.Id) && u.IsActive);
            if (existUser == null)
                throw new UserNotFoundException(Messages.UserNotFound);
            var avatarsDbo = _context.Avatars.Where(a => a.User == existUser && a.IsActive).ToList();
            double result = 0;
            double resultNow = 0;
            string userCompareId = string.Empty;
            string userCompareName = string.Empty;
            foreach (var userDto in users)
            {
                var existUserCompare = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(userDto.Id) && u.IsActive);
                if (existUserCompare != null)
                {
<<<<<<< HEAD
                    if (existUser.Id != existUserCompare.Id)
                    {
                        var avatarsCompareDbo = _context.Avatars.Where(a => a.User == existUserCompare && a.IsActive).ToList();

                        result = Compare(avatarsDbo, avatarsCompareDbo);

                        if (result > resultNow)
                        {
                            resultNow=result;
                            userCompareId = existUserCompare.Id.ToString();
                            userCompareName = $"{existUserCompare.FirstName} {existUserCompare.LastName}";
                        }
                    }
                }
            }
            return $"Стиль ввода {existUser.FirstName} {existUser.LastName} максимально совпадает с {userCompareName}. Процент совпадения : {resultNow * 100} %";
=======
                    var avatarsCompareDbo = _context.Avatars.Where(a => a.User == existUserCompare && a.IsActive).ToList();

                    result = Compare(avatarsDbo, avatarsCompareDbo);

                    if (result < resultNow)
                    {
                        result = resultNow;
                        userCompareId = existUserCompare.Id.ToString();
                        userCompareName = $"{existUserCompare.FirstName} {existUserCompare.LastName}";
                    }
                }
            }
            return $"Стиль ввода {existUser.FirstName} {existUser.LastName} максимально совпадает с {userCompareName}. Процент совпадения : {result * 100} %";
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
        }

        public string CompareWithUser(User user, User userCompare)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(user.Id) && u.IsActive);
            if (existUser == null)
                throw new UserNotFoundException(Messages.UserNotFound);
            var existUserCompare = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(userCompare.Id) && u.IsActive);
            if (existUserCompare == null)
                throw new UserNotFoundException(Messages.UserNotFound);
            var avatarsDbo = _context.Avatars.Where(a => a.User == existUser && a.IsActive).ToList();
            var avatarsCompareDbo = _context.Avatars.Where(a => a.User == existUserCompare && a.IsActive).ToList();
            var result = Compare(avatarsDbo, avatarsCompareDbo);
            return $"Стиль ввода {existUser.FirstName} {existUser.LastName} и {existUserCompare.FirstName} {existUserCompare.LastName} совпадают на : {result * 100} %";
        }

        private double Compare(List<AvatarDto> Avatar, List<AvatarDto> AvatarCheck)
        {
            double proc = 0, obsh = 0;
            foreach (var item1 in Avatar)
            {
                foreach (var item2 in AvatarCheck)
                {
                    if (item1.CharPair == item2.CharPair)
                    {
                        proc += Math.Abs(item1.Delay - item2.Delay);
                        obsh += item1.Delay;
                        break;
                    }
                }
            }
            proc = 1 - proc / obsh;
            return proc;
        }

<<<<<<< HEAD
        public string CompareWithRandomUser(User user, IEnumerable<Avatar> avatars)
        {
            var existUser = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(user.Id) || u.IsActive);
            if (existUser == null)
                throw new UserNotFoundException(Messages.UserNotFound);
            var avatarsDbo = _context.Avatars.Where(a => a.User == existUser && a.IsActive).ToList();
            var avatarslist = new List<Avatar>(avatars);
            var avatarsTemporary = OptimizationAvatar(avatarslist);
            var avatarsTemporaryList = new List<AvatarDto>();
            foreach (var item in avatarsTemporary)
            {
                avatarsTemporaryList.Add(new AvatarDto()
                {
                    CharPair = item.CharPair,
                    Delay = item.Delay,
                    Identical = item.Identical
                });
            }
            if (Compare(avatarsDbo, avatarsTemporaryList) > 0.8)
            {
                return $"Это вы!";
            }
            else
                return $"Пожалуйста, покиньте чужой аккаунт";

        }
=======
        //private string CompareWithTemporaryAvatar(User user, IEnumerable<Avatar> avatars)
        //{
        //    var existUser = _context.Users.FirstOrDefault(u => u.Id == Guid.Parse(user.Id) || u.IsActive);
        //    if (existUser == null)
        //        throw new UserNotFoundException(Messages.UserNotFound);
        //    var avatarsDbo = _context.Avatars.Where(a => a.User == existUser && a.IsActive).ToList();
        //    var avatarslist = new List<Avatar>(avatars);
        //    var avatarsTemporary = OptimizationAvatar(avatarslist);
        //    OptimizationAvatar(avatarsTemporary);
        //    if (Compare(avatarsDbo, avatarsTemporary) > 0.8)
        //    {
        //        return $"Это вы!";
        //    }
        //    else
        //        return $"Пожалуйста, покиньте чужой аккаунт";

        //}
>>>>>>> 90ebc9c8c12f12b888911c5fdf2c90f5877da5bb
    }
}
