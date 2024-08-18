using CBData.Task.Core.Main;
using CBData.Task.Core.Orders;
using Moq;

namespace CBData.Task.Tests
{
    //there is not much logic in the task so unit tests are kinda unnecessary as they will mostly test mocks
    public sealed class OrdersTest
    {
        private readonly Mock<IDataAccess> _dataAccess;
        private readonly OrdersRepo _ordersRepo;

        public OrdersTest()
        {
            _dataAccess = new Mock<IDataAccess>();
            _dataAccess.Setup(x => x.AddOrder(It.IsAny<Order>()));
            _ordersRepo = new(_dataAccess.Object);
        }

        [Fact]
        public void OrdersMapper_Map_ShouldSuccess()
        {
            List<OrderDTO> orders =
            [
                new()
                {
                    ProductId = "456",
                    Quantity = 5
                },
                new()
                {
                    ProductId = "789",
                    Quantity = 42
                }
            ];

            List<Order> expected =
            [
                new()
                {
                    ProductId = "456",
                    Quantity = 5
                },
                new()
                {
                    ProductId = "789",
                    Quantity = 42
                }
            ];

            var actual = OrdersMapper.Map(orders);
            Assert.Equal(expected, actual.Value);
            Assert.True(actual.IsOk);
        }

        [Fact]
        public void OrdersMapper_Map_ShouldError()
        {
            List<OrderDTO> orders =
            [
                new()
                {
                    ProductId = "456",
                    Quantity = 5
                },
                new()
                {
                    ProductId = "789"
                }
            ];

            var actual = OrdersMapper.Map(orders);
            Assert.Equal(System.Net.HttpStatusCode.BadRequest, actual.Error!.HttpStatusCode);
            Assert.False(string.IsNullOrEmpty(actual.Error.Message));
            Assert.False(actual.IsOk);
        }

        [Fact]
        public void OrdersRepo_AddData_ShouldWork()
        {
            List<Order> data =
            [
                new()
                {
                    ProductId = "456",
                    Quantity = 5
                },
                new()
                {
                    ProductId = "789",
                    Quantity = 42
                }
            ];
            _ordersRepo.AddData(data);
            _dataAccess.Verify(x => x.AddOrder(It.IsAny<Order>()), Times.Exactly(2));
        }

        [Fact]
        public void OrdersRepo_AddData_ShouldError()
        {
            ResultException result = new()
            {
                HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                Message = "this is a message"
            };
            _ordersRepo.AddData(result);

            Assert.Equal(System.Net.HttpStatusCode.BadRequest, result.HttpStatusCode);
            Assert.False(string.IsNullOrEmpty(result.Message));
        }
    }
}