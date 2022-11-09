<template>

  <body>
    <div class="card text-light bgimg d-flex card-shadow" :style="{ backgroundImage: `url(${keeps.img})` }">
      <div class=" text-end p-1">
        <i @click="deleteKeep()" v-if="keeps.creatorId == profile.id" title="Delete"
          class="mdi mdi-delete-forever selectable text-danger"></i>
      </div>
      <div class="card-body">
      </div>
      <div class="card-footer outline d-flex justify-content-between text-shadow">
        <h3 class="selectable" data-bs-toggle="modal" :data-bs-target="`#vaultKeep${keeps.id}`">
          {{ keeps.name }}
        </h3>
        <router-link :to="{ name: 'Profile', params: { id: keeps.creatorId } }">
          <img :title=keeps.creator?.name :src=profile.picture alt="profile img" class="profile-img">
        </router-link>
      </div>
    </div>
  </body>

  <!-- View Keep Modal -->
  <div class="modal fade" :id="`vaultKeep${keeps.id}`" tabindex="-1" aria-labelledby="Vault Keep" aria-hidden="true">
    <div class="modal-dialog modal-xl modal-dialog-centered">
      <div class="modal-content">
        <div class="no-pad modal-body">
          <div class="container-fluid">
            <div class="row">
              <div class="col-md-6 no-pad">
                <img :src=keeps.img alt="Keep Image" class="img-fluid">
              </div>
              <div class="col-md-6 text-center d-flex flex-column justify-content-between px-5 py-1">
                <div class="d-flex justify-content-center">
                  <h5 class="mdi mdi-eye-outline">&nbsp{{ keeps.views }}&nbsp&nbsp</h5>
                  <h5 class="mdi mdi-alpha-k-box-outline">&nbsp{{ keeps.kept }}</h5>
                </div>
                <div>
                  <h1>{{ keeps.name }} </h1>
                  <p class="text-start">{{ keeps.description }} Lorem ipsum dolor sit amet consectetur adipisicing elit.
                    Accusamus animi
                    illo nulla, maxime repudiandae saepe molestiae alias excepturi odio, quo officiis repellendus,
                    corrupti rem est vero iste non blanditiis? Odit.</p>
                </div>
                <!-- Add Keep to Vault -->
                <div class="d-flex justify-content-between">
                  <div class="my-5 d-flex gap-3">
                    <select class="form-select form-select-sm fs-5" aria-label=".form-select-sm">
                      <option selected>Select Vault</option>
                      <option value="1">One</option>
                      <option value="2">Two</option>
                      <option value="3">Three</option>
                    </select>
                    <button class="btn btn-primary">Save</button>
                  </div>
                  <img :src=profile.picture alt="profile pic" :title=profile.name class="profile-img mt-5">
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>

</template>


<script>
import { computed } from "@vue/reactivity";
import { AppState } from "../AppState";
import { Keep } from "../models/Keep";
import { keepsService } from "../services/KeepsService";
import Pop from "../utils/Pop";

export default {
  props: {
    keeps: { type: Keep, required: true }
  },
  setup(props) {
    return {
      profile: computed(() => AppState.account),
      vault: computed(() => AppState.vaults),
      async deleteKeep() {
        try {
          if (await Pop.confirm("Delete Keep?"))
            await keepsService.deleteKeep(props.keeps.id)
        } catch (error) {
          Pop.error(error, "Deleting Keep")
        }
      },

      async addKeepToVault() {
        try {

        } catch (error) {
          Pop.error(error, "Adding keep to vault")
        }
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.bgimg {
  height: 35vh;
  background-position: center;
  background-size: cover;
}

.outline {
  border: none;
  background-color: #ffffff00
}

.profile-img {
  border-radius: 50%;
  max-height: 5vh;
}

.card-shadow {
  box-shadow: 0 2px 10px -1px #00000033, 0 5px 10px 0 #00000070;
}

.text-shadow {
  text-shadow: 1px 1px 5px #ff00e1, 0 0 1em #8888f7, 0 0 0.2em #ffffff;
}
</style>