const uri = "http://localhost:5000/";

async function getDataUser(nickname) {
    const res = await fetch(`${uri}api/profile/${nickname}`, {
        method: "GET",
        // headers: {
        //     Authorization: token
    });
    return await res;
}

async function deleteLink(token, type, url, name) {
    const res = await fetch(`${uri}api/profile/link`, {
        method: "DELETE",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': token
        },
        body: JSON.stringify({
            type: type,
            url: url,
            name: name
        })
    });
    return await res;
}

async function addLink(token, type, url, name) {
    const res = await fetch(`${uri}api/profile/link`, {
        method: "Post",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': token
        },
        body: JSON.stringify({
            type: type,
            url: url,
            name: name
        })
    });
    return await res;
}

async function deleteTag(token, id, name, color) {
    const res = await fetch(`${uri}api/profile/tag`, {
        method: "DELETE",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': token
        },
        body: JSON.stringify({
            id: id,
            name: name,
            color: color
        })
    });
    return await res;
}

async function addTag(token, id, name, color) {
    const res = await fetch(`${uri}api/profile/tag`, {
        method: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': token
        },
        body: JSON.stringify({
            id: id,
            name: name,
            color: color
        })
    });
    return await res;
}

async function addAbout(token, newAbout) {
    const res = await fetch(`${uri}api/profile/about`, {
        method: "PUT",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': token
        },
        body: JSON.stringify({
            newAbout: newAbout
        })
    });
    return await res;
}

async function addPhoto(token, base64) {
    const res = await fetch(`${uri}api/profile/photo`, {
        method: "PUT",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': token
        },
        body: JSON.stringify({
            base64: base64
        })
    });
    console.log(res);
    return await res;
}

async function getAllTags() {
    const res = await fetch(`${uri}api/tag`, {
        method: "GET",
    });
    return await res;
}

module.exports = { getDataUser, deleteLink, deleteTag, addLink, addTag, getAllTags, addAbout, addPhoto }