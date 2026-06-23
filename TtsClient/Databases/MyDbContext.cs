using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using TtsClient.Models;
using TtsClient.Utils;

namespace TtsClient.Databases
{
    public class MyDbContext : DbContext
    {
        public DbSet<SpeechEntry> SpeechEntries { get; set; }

        public DbSet<SpeechMetadata> SpeechMetadata { get; set; }

        public DbSet<AudioFileEntry> AudioFileEntries { get; set; }

        public DbSet<AudioMetadata> AudioMetadata { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var baseDir = AppContext.BaseDirectory;
                var dbPath = Path.Combine(baseDir, "speech_entries.db");
                Logger.Log($"DB Path: {dbPath}");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}