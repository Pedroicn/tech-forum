using TechForum.Business.Models;
namespace TechForum.Tests;

public class UnitTest1
{
    [Fact]
    public void Add_Topic_IncreaseTopicAmount()
    {
        //Arrange
        var newUser = new User("Pedro", "pedro@gmail.com", "pedro$123");
        var newTopic = new Topic(newUser.Id, "teste", "isso é um teste");
        //Act
        newUser.AddTopics(newTopic);
        //Assert
        Assert.Equal(1, newUser.TopicAmount);
        Assert.Equal(1, newUser.topics.Count);
    }

    [Fact]
    public void Add_Comment_IncreaseCommentAmount()
    {
        //Arrange
        var newUser = new User("Pedro", "pedro@gmail.com", "pedro$123");
        var newTopic = new Topic(newUser.Id, "teste", "isso é um teste");
        var newComment = new Comment(newUser, "this is a comment");
        //Act
        newTopic.AddComments(newComment);
        //Assert
        Assert.Equal(1, newTopic.CommentAmount);
        Assert.Equal(1, newTopic.Comments.Count);
        Assert.Equal("this is a comment", newComment.Description);
    }
}