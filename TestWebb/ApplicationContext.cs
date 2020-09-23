using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebb.Models;

namespace TestWebb
{
    public class ApplicationContext : DbContext
    {
        public DbSet<UserData> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserChat> UserChats { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<KeyStorage> Keys { get; set; }

        public DbSet<BanList> Bans { get; set; }
        public ApplicationContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();

            UserData user = new UserData();
            user.Login = "ivan";
            user.Password = "2w3e4r";

            Users.Add(user);
            UserData user1 = new UserData();
            user1.Login = "anton";
            user1.Password = "56789";
            Users.Add(user1);

            UserData user2 = new UserData();
            user2.Login = "alex";
            user2.Password = "0987ew";
            Users.Add(user2);
            SaveChanges();

            Token token = new Token();
            token.UserId = user.Id;
            token.TokenId = "dferw";
            Tokens.Add(token);

            Token token1 = new Token();
            token1.UserId = user1.Id;
            token1.TokenId = "2w3e4r";
            Tokens.Add(token1);

            Token token2 = new Token();
            token2.UserId = user2.Id;
            token2.TokenId = "98765";
            Tokens.Add(token2);
            SaveChanges();

            Chat chat = new Chat();
            chat.Name = "Geography";
            chat.UserId = user.Id;

            Chats.Add(chat);
            SaveChanges();

            UserChat userChat = new UserChat();
            userChat.ChatId = chat.Id;
            userChat.UserId = user.Id;
            UserChats.Add(userChat);
            SaveChanges();

            UserChat userChat1 = new UserChat();
            userChat1.ChatId = chat.Id;
            userChat1.UserId = user1.Id;
            UserChats.Add(userChat1);
            SaveChanges();

            UserChat userChat2 = new UserChat();
            userChat2.ChatId = chat.Id;
            userChat2.UserId = user2.Id;
            UserChats.Add(userChat2);
            SaveChanges();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "MyDb.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserData>()
           .HasOne(e => e.Profile).WithOne(e => e.UserData)
           .HasForeignKey<Profile>(e => e.UserId);

            modelBuilder.Entity<UserChat>()
           .HasKey(t => new { t.UserId, t.ChatId });

            modelBuilder.Entity<UserChat>()
                .HasOne(sc => sc.User)
                .WithMany(s => s.UserChats)
                .HasForeignKey(sc => sc.UserId);

            modelBuilder.Entity<UserChat>()
                .HasOne(sc => sc.Chat)
                .WithMany(c => c.UserChats)
                .HasForeignKey(sc => sc.ChatId);

            modelBuilder.Entity<BanList>()
            .HasKey(o => new { o.UserId, o.ChatId });

            //  modelBuilder.Entity<UserData>()
            //.HasOne(e => e.Token).WithOne(e => e.UserData)
            //.HasForeignKey<Token>(e => e.UserId);
            modelBuilder.Entity<Profile>().ToTable("Profiles");
            modelBuilder.Entity<UserData>().ToTable("Users");
            modelBuilder.Entity<Token>().ToTable("Tokens");

        }
    }
}
