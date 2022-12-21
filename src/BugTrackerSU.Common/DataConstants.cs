namespace BugTrackerSU.Common
{
    public class DataConstants
    {
        // Category
        public const int CategoryNameMinLength = 4;
        public const int CategoryNameMaxLength = 64;

        public const int CategoryDescriptionMinLength = 12;
        public const int CategoryDescriptionMaxLength = 4096;

        // Article
        public const int ArticleNameMinLength = 4;
        public const int ArticleNameMaxLength = 64;

        public const int ArticleDescriptionMinLength = 12;
        public const int ArticleDescriptionMaxLength = 4096;

        // Project - "DataModel"
        public const int ProjectTitleMinLength = 4;
        public const int ProjectTitleMaxLength = 64;

        public const int ProjectDescriptionMinLength = 8;
        public const int ProjectDescriptionMaxLength = 4096;

        // Ticket
        public const int TicketTitleMinLength = 4;
        public const int TicketTitleMaxLength = 64;

        public const int TicketStatusMinLength = 4;
        public const int TicketStatusMaxLength = 16;

        public const int TicketPriorityMinLength = 4;
        public const int TicketPriorityMaxLength = 10;

        public const int TicketDescriptionMinLength = 8;
        public const int TicketDescriptionMaxLength = 4096;

        public const int TicketTypeMinLength = 3;
        public const int TicketTypeMaxLength = 50;

        // Task
        public const int TaskTitleMinLength = 4;
        public const int TaskTitleMaxLength = 40;

        public const int TaskContentMinLength = 4;
        public const int TaskContentMaxLength = 496;

        public const int TaskTypeMinLength = 4;
        public const int TaskTypeMaxLength = 50;

        // Comment
        public const int CommentContentMinLength = 4;
        public const int CommentContentMaxLength = 496;

        // Post
        public const int PostTitleMinLength = 4;
        public const int PostTitleMaxLength = 40;

        public const int PostContentMinLength = 4;
        public const int PostContentMaxLength = 496;
    }
}
