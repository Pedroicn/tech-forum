using TechForum.Business.Models;
namespace TechForum.Tests;

public class TestTopicMethods
{
    [Fact(DisplayName = "Given valid inputs, and add a new topic, TopicAmount should increase")]
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
    public void Delete_Topic_DecreaseTopicAmount()
    {
        //Arrange
        var newUser = new User("Pedro", "pedro@gmail.com", "pedro$123");
        var newTopic = new Topic(newUser.Id, "teste", "isso é um teste");

        //Act
        newUser.AddTopics(newTopic);
        newUser.DeleteTopic(newTopic);
        //Assert
        Assert.Equal(0, newUser.TopicAmount);
        Assert.Equal(0, newUser.topics.Count);
    }

}

public class TestCommentMethods
{
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

    [Fact]
    public void Delete_Comment_DecreaseCommentAmount()
    {
        //Arrange
        var newUser = new User("Pedro", "pedro@gmail.com", "pedro$123");
        var newTopic = new Topic(newUser.Id, "teste", "isso é um teste");
        var newComment = new Comment(newUser, "this is a comment");
        //Act
        newUser.AddTopics(newTopic);
        newTopic.AddComments(newComment);
        newTopic.DeleteComment(newComment);
        //Assert
        Assert.Equal(0, newTopic.CommentAmount);
        Assert.Equal(0, newTopic.Comments.Count);
    }
}