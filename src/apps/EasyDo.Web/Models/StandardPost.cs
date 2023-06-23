using Piranha.AttributeBuilder;
using Piranha.Models;

namespace EasyDo.Web.Models;

[PostType(Title = "Standard post")]
public class StandardPost  : Post<StandardPost>
{
}
