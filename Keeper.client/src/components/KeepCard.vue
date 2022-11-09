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
              <div class="col-md-6 text-center">
                <h1>{{ keeps.name }}</h1>
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

      async deleteKeep() {
        try {
          if (await Pop.confirm("Delete Keep?"))
            await keepsService.deleteKeep(props.keeps.id)
        } catch (error) {
          Pop.error(error, "Deleting Keep")
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