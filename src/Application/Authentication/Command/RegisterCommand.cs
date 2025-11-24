using System;
using MediatR;
using Shared.Common;
using Shared.DTOs;

namespace Application.Authentication.Command;

public class RegisterCommand : IRequest<ResponseModel>
{
    public UserDTO user { get; }
    public UserAuthenticationDTO userAuthentication { get; }
    public List<AddressDTO>? userAddresses { get; }
    public List<ContactDTO>? userContact { get; }

    public RegisterCommand(UserDTO user, UserAuthenticationDTO userAuthentication, List<AddressDTO>? userAddresses, List<ContactDTO>? userContact)
    {
        this.user = user;
        this.userAuthentication = userAuthentication;
        this.userAddresses = userAddresses;
        this.userContact = userContact;
    }
}
