const backend = "http://localhost:5000";

async function getDataProject(id, token) {
  let address = backend + "/api/project/" + id;
  const res = await fetch(address, {
    method: "GET",
    headers: {
      'Authorization': 'Bearer ' + token
    }
  });
  return await res;
}

async function addLike(id, token, dislike) {
  let method = 'POST';
  if (dislike == 1)
    method = 'PUT';
  const res = await fetch(backend + "/api/project/" + id + '/rating', {
    method: method,
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({ rating: 1 })
  });
  return await res;
}

async function removeLike(id, token) {
  const res = await fetch(backend + "/api/project/" + id + '/rating', {
    method: "DELETE",
    headers: {
      'Accept': 'application/json;charset=utf-8',
      'Content-Type': 'application/json;charset=utf-8',
      'Authorization': 'Bearer ' + token
    }
  });
  return await res;
}

async function addDislike(id, token, like) {
  let method = 'POST';
  if (like == 1)
    method = 'PUT';
  const res = await fetch(backend + "/api/project/" + id + '/rating', {
    method: method,
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({ rating: 0 })
  });
  return await res;
}

async function removeDislike(id, token) {
  const res = await fetch(backend + "/api/project/" + id + '/rating', {
    method: "DELETE",
    headers: {
      'Accept': 'application/json;charset=utf-8',
      'Content-Type': 'application/json;charset=utf-8',
      'Authorization': 'Bearer ' + token
    }
  });
  return await res;
}

async function saveTitle(id, token, newTitle) {
  const res = await fetch(backend + "/api/project/" + id + '/name', {
    method: "PUT",
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({ newName: newTitle })
  });
  return await res;
}

async function saveMarkdown(id, token, newMd) {
  const res = await fetch(backend + "/api/project/" + id + '/markdown', {
    method: "PUT",
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({ newMarkdown: newMd })
  });
  return await res;
}

async function publishNews(id, token, newsTitle, newsContents) {
  const res = await fetch(backend + "/api/project/" + id + '/news', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({ name: newsTitle, content: newsContents })
  });
  return await res;
}

async function addMedia(id, token, file, type) {
  const res = await fetch(backend + "/api/project/" + id + '/media', {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({ link: file, type: type })
  });
  return await res;
}

async function removeMedia(id, token, file, type) {
  const res = await fetch(backend + "/api/project/" + id + '/media', {
    method: 'DELETE',
    headers: {
      'Accept': 'application/json;charset=utf-8',
      'Content-Type': 'application/json;charset=utf-8',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({ link: file, type: type })
  });
  return await res;
}

async function addTag(id, token, tagId, tagName, tagColor) {
  const res = await fetch(backend + "/api/project/" + id + "/tag", {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({
      id: tagId,
      name: tagName,
      color: tagColor
    })
  });
  return await res;
}

async function removeTag(id, token, tagId, tagName, tagColor) {
  const res = await fetch(backend + "/api/project/" + id + "/tag", {
    method: 'DELETE',
    headers: {
      'Accept': 'application/json;charset=utf-8',
      'Content-Type': 'application/json;charset=utf-8',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({
      id: tagId,
      name: tagName,
      color: tagColor
    })
  });
  return await res;
}

async function sendRequest(id, token, message) {
  const res = await fetch(backend + "/api/request/fromUser", {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({
      projectId: id,
      message: message,
      type: 1
    })
  });
  return await res;
}

async function getRequests(id, token) {
  const res = await fetch(backend + "/api/request/project/" + id, {
    method: 'GET',
    headers: {
      'Authorization': 'Bearer ' + token
    }
  });
  return await res;
}

async function acceptRequest(projId, token, devId) {
  const res = await fetch(backend + "/api/request/accept", {
    method: 'POST',
    headers: {
      'Accept': 'application/json',
      'Content-Type': 'application/json',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({
      "developerId": devId,
      "projectId": projId,
      "message": "",
      "type": 1
    })
  });
  return await res;
}

async function declineRequest(projId, token, devId) {
  const res = await fetch(backend + "/api/request/decline", {
    method: 'DELETE',
    headers: {
      'Accept': 'application/json;charset=utf-8',
      'Content-Type': 'application/json;charset=utf-8',
      'Authorization': 'Bearer ' + token
    },
    body: JSON.stringify({
      "developerId": devId,
      "projectId": projId,
      "message": "",
      "type": 1
    })
  });
  return await res;
}

module.exports = {
  getDataProject, addLike, removeLike, addDislike, removeDislike,
  saveTitle, saveMarkdown, publishNews, addMedia, removeMedia,
  addTag, removeTag, sendRequest, getRequests, acceptRequest, declineRequest
}