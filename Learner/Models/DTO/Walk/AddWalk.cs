namespace Learner.Models.DTO.Walk
{
    public class AddWalk
    {

        public string Name { get; set; }
        public double Length { get; set; }
        public Guid regionID { get; set; }
        public Guid WalkDifficultyID { get; set; }
    }
}
